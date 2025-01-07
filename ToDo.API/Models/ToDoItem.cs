namespace ToDo.API.Models;

public sealed class ToDoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public bool IsCompleted { get; set; }
}
