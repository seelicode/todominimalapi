namespace TodoMinimalApi.Model
{
    public class ToDo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public string TaskDescription { get; set; } = "New Todo";
        public bool IsCompleted { get; set; }
    }

    public class ToDoCreateModel
    {
        public string? TaskDescription { get; set; }
    }

    public class ToDoUpdateModel
    {
        public string? TaskDescription { get; set; }
        public bool IsCompleted { get; set; }
    }
}
