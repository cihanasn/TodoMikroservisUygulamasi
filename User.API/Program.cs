using Microsoft.EntityFrameworkCore;
using User.API.Context;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL baðlantýsý ve DbContext yapýlandýrmasý
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Kullanýcýlar için CRUD iþlemleri
app.MapGet("/api/users", async (AppDbContext db, CancellationToken cancellationToken) =>
{
    return await db.Users.ToListAsync(cancellationToken);
});

app.MapPost("/api/users", async (AppDbContext db, User.API.Models.User user, CancellationToken cancellationToken) =>
{
    await db.Users.AddAsync(user, cancellationToken);
    await db.SaveChangesAsync(cancellationToken);
    return Results.Created($"/api/users/{user.Id}", new { message = "Kullanýcý baþarýyla eklendi.", user });
});

// Migration'larý otomatik olarak uygula
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync(); // Migration'larý uygula
}

app.Run();
