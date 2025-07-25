using Microsoft.EntityFrameworkCore;
using TodoAPP.Models;

namespace TodoAPP.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<TodoItem> Todos => Set<TodoItem>();
}
