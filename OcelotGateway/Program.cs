using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Ocelot yapýlandýrmasý için JSON dosyasýný ekle
builder.Configuration.AddJsonFile("ocelot.json");

// CORS politikasý ekle
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
