using Microsoft.EntityFrameworkCore;
using User.API.Context;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL ba�lant�s� ve DbContext yap�land�rmas�
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Kullan�c�lar i�in CRUD i�lemleri
app.MapGet("/api/users", async (AppDbContext db, CancellationToken cancellationToken) =>
{
    return await db.Users.ToListAsync(cancellationToken);
});

app.MapPost("/api/users", async (AppDbContext db, User.API.Models.User user, CancellationToken cancellationToken) =>
{
    await db.Users.AddAsync(user, cancellationToken);
    await db.SaveChangesAsync(cancellationToken);
    return Results.Created($"/api/users/{user.Id}", new { message = "Kullan�c� ba�ar�yla eklendi.", user });
});

// Migration'lar� otomatik olarak uygula
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync(); // Migration'lar� uygula
}

app.Run();
