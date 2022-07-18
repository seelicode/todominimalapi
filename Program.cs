using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Data;
using TodoMinimalApi.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sqlConnectionBuilder = new SqlConnectionStringBuilder();
sqlConnectionBuilder.ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");

bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

if (isDevelopment)
{
    sqlConnectionBuilder.UserID = builder.Configuration["UserId"];
    sqlConnectionBuilder.Password = builder.Configuration["Password"];
}

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConnectionBuilder.ConnectionString));
builder.Services.AddScoped<IToDoRepository, ToDoRepository>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// define Mappings for GET, POST, PUT and DELETE 

app.MapGet("api/todos", async (IToDoRepository repo) => {
    var toDos = await repo.GetAllToDos();
    return Results.Ok(toDos);
});

app.MapGet("api/todos/{id}", async (IToDoRepository repo, Guid id) => {
    var toDo = await repo.GetToDoById(id);
    if (toDo != null)
    {
        return Results.Ok(toDo);
    }
    return Results.NotFound();
});

app.MapPost("api/todos", async (IToDoRepository repo, ToDoCreateModel createToDo) => {

    var toDo = new ToDo();
    toDo.TaskDescription = createToDo.TaskDescription;

    await repo.CreateToDo(toDo);
    await repo.SaveChanges();

    return Results.Created($"api/todos/{toDo.Id}", toDo);

});

app.MapPut("api/todos/{id}", async (IToDoRepository repo, Guid id, ToDoUpdateModel updateToDo) => {
    var toDo = await repo.GetToDoById(id);
    if (toDo == null)
    {
        return Results.NotFound();
    }

    if (!string.IsNullOrWhiteSpace(toDo.TaskDescription))
    {
        toDo.TaskDescription = updateToDo.TaskDescription;
    }
    
    toDo.IsCompleted = updateToDo.IsCompleted;

    await repo.SaveChanges();

    return Results.NoContent();
});

app.MapDelete("api/todos/{id}", async (IToDoRepository repo, Guid id) => {
    var toDo = await repo.GetToDoById(id);
    if (toDo == null)
    {
        return Results.NotFound();
    }

    repo.DeleteToDo(toDo);

    await repo.SaveChanges();

    return Results.NoContent();

});

app.Run();

