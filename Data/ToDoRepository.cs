using Microsoft.EntityFrameworkCore;
using TodoMinimalApi.Model;

namespace TodoMinimalApi.Data
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _dbContext;

        public ToDoRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ToDo?> GetToDoById(Guid id)
        {
            return await _dbContext.ToDos.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ToDo>> GetAllToDos()
        {
            return await _dbContext.ToDos.ToListAsync();
        }

        public async Task CreateToDo(ToDo toDo)
        {
            if (toDo == null)
            {
                throw new ArgumentNullException(nameof(toDo));
            }

            await _dbContext.AddAsync(toDo);
        }

        public void DeleteToDo(ToDo toDo)
        {
            if (toDo == null)
            {
                throw new ArgumentNullException(nameof(toDo));
            }

            _dbContext.Remove(toDo);
        }
    }
}
