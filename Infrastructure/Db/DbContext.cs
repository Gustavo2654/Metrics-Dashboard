using MDashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MDashboard.Infrastructure.Db;

public class DbContexto : DbContext
{
    public DbSet<Sale> Sales { get; set; }

    public DbContexto(DbContextOptions<DbContexto> options) : base(options)
    {
    }
}