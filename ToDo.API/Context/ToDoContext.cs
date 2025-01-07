using Microsoft.EntityFrameworkCore;
using ToDo.API.Models;

namespace ToDo.API.Context;

public sealed class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

    public DbSet<ToDoItem> ToDoItems { get; set; }
}
