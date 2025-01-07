using Microsoft.EntityFrameworkCore;

namespace User.API.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSet, veritabanındaki tabloları temsil eder
    public DbSet<User.API.Models.User> Users { get; set; }
}
