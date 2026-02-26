using MDashboard.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace MDashboard.Domain.Controllers;

// Controller para métricas de vendas
[ApiController]
[Route("api/[controller]")]
public class MetricsController : ControllerBase
{
    private readonly MetricsService _metricsService;

    public MetricsController(MetricsService metricsService)
    {
        _metricsService = metricsService;
    }

    // Endpoint para obter métricas de vendas por dia
    [HttpGet("sales")]
    public IActionResult GetSalesByDay()
    {
        var q = Request.Query;
        string? start = q.ContainsKey("start") ? q["start"].FirstOrDefault() : q.ContainsKey("startDate") ? q["startDate"].FirstOrDefault() : null;
        string? end = q.ContainsKey("end") ? q["end"].FirstOrDefault() : q.ContainsKey("endDate") ? q["endDate"].FirstOrDefault() : null;

        DateTime startDate = DateTime.MinValue;
        DateTime endDate = DateTime.MaxValue;

        if (!string.IsNullOrWhiteSpace(start))
        {
            if (!TryParseDate(start, out startDate))
                return BadRequest("Formato de data 'start' inválido. Use yyyy-MM-dd ou dd/MM/yyyy.");
        }

        if (!string.IsNullOrWhiteSpace(end))
        {
            if (!TryParseDate(end, out endDate))
                return BadRequest("Formato de data 'end' inválido. Use yyyy-MM-dd ou dd/MM/yyyy.");
        }

        if (startDate > endDate)
            return BadRequest("Data inicial maior que final.");

        var result = _metricsService.GetSalesByDay(startDate, endDate);

        return Ok(result);
    }

    // Método auxiliar para tentar parsear datas em múltiplos formatos
    private static bool TryParseDate(string input, out DateTime date)
    {
        var formats = new[] { "yyyy-MM-dd", "yyyy/MM/dd", "dd/MM/yyyy", "dd-MM-yyyy", "MM/dd/yyyy", "yyyyMMdd" };
        if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            return true;

        if (DateTime.TryParse(input, CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
            return true;

        if (DateTime.TryParse(input, CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None, out date))
            return true;

        return false;
    }
}