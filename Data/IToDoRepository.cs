using TodoMinimalApi.Model;

namespace TodoMinimalApi.Data;

public interface IToDoRepository
{
    Task SaveChanges();
    Task<ToDo?> GetToDoById(Guid id);
    Task<IEnumerable<ToDo>> GetAllToDos();
    Task CreateToDo(ToDo toDo);

    void DeleteToDo(ToDo toDo);
}

