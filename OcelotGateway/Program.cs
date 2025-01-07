using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Ocelot yap�land�rmas� i�in JSON dosyas�n� ekle
builder.Configuration.AddJsonFile("ocelot.json");

// CORS politikas� ekle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Angular projenizin URL'si
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Ocelot servislerini ekle
builder.Services.AddOcelot();

var app = builder.Build();

// CORS middleware'i ekle
app.UseCors("AllowAngularApp");

app.MapGet("/", () => "Hello World!");

// Ocelot Middleware'i kullan
app.UseOcelot().Wait();

app.Run();
