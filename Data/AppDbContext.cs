using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Model;

namespace TodoMinimalApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ToDo> ToDos => Set<ToDo>();
}

