using Microsoft.EntityFrameworkCore;
using TodoAPP.Data;
using TodoAPP.Repositories;

var builder = WebApplication.CreateBuilder(args);

// EF + Connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Repository
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddControllers();

// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Cổng của frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build(); // 🔥 Đây là phần bị thiếu khiến bạn lỗi

// Áp dụng CORS
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
