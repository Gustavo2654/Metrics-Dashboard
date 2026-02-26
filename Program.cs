using MDashboard.Domain.Services;
using MDashboard.Domain.Entities;
using MDashboard.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<MetricsService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbContexto>(options =>
    options.UseSqlite("Data Source=sales.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Garantir que o banco de dados seja criado e populado com dados de exemplo
using (var scope = app.Services.CreateScope())
{
    var contexto = scope.ServiceProvider.GetRequiredService<DbContexto>();
    contexto.Database.EnsureCreated();
    if (!contexto.Sales.Any())
    {
        contexto.Sales.Add(new Sale
        {
            Value = 100.50m,
            Date = new DateTime(2024, 1, 1),
            Category = "Electronics"
        });
        contexto.SaveChanges();
    }
}

app.UseAuthorization();
app.MapControllers();
app.Run();