using Microsoft.EntityFrameworkCore;
using ToDo.API.Context;
using ToDo.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoContext>(options => options.UseInMemoryDatabase("ToDoDb"));

var app = builder.Build();

app.MapGet("/api/todos", async (ToDoContext context, CancellationToken cancellationToken) =>
{
    return await context.ToDoItems.ToListAsync(cancellationToken);
});

app.MapPost("/api/todos", async (ToDoContext context, ToDoItem item, CancellationToken cancellationToken) =>
{
    await context.ToDoItems.AddAsync(item, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);
    return Results.Created($"/api/todos/{item.Id}", new { message = "Yapýlacak öðesi baþarýyla oluþturuldu.", item });
});

app.Run();
