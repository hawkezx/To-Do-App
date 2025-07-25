using Microsoft.EntityFrameworkCore;
using TodoAPP.Data;
using TodoAPP.Models;
using TodoAPP.Repositories;

namespace TodoAPP.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
        => await _context.Todos.ToListAsync();

    public async Task<TodoItem?> GetByIdAsync(int id)
        => await _context.Todos.FindAsync(id);

    public async Task AddAsync(TodoItem item)
    {
        _context.Todos.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem item)
    {
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TodoItem item)
    {
        _context.Todos.Remove(item);
        await _context.SaveChangesAsync();
    }
}
