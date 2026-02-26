using MDashboard.Domain.DTOs;
using MDashboard.Infrastructure.Db;

namespace MDashboard.Domain.Services;

// Serviço para calcular métricas de vendas
public class MetricsService
{
    private readonly DbContexto _contexto;
    public MetricsService(DbContexto contexto)
    {
        _contexto = contexto;
    }
    // Método para obter métricas de vendas por dia
    public List<SalesMetricDTO> GetSalesByDay(DateTime start, DateTime end)
    {
        return _contexto.Sales
            .Where(s => s.Date >= start && s.Date <= end)
            .AsEnumerable()
            .GroupBy(s => s.Date.Date)
            .Select(g => new SalesMetricDTO
            {
                Date = g.Key.ToString("yyyy-MM-dd"),
                Total = g.Sum(x => x.Value),
                Count = g.Count()
            })
            .OrderBy(x => x.Date)
            .ToList();
    }
}