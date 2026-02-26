namespace MDashboard.Domain.DTOs;

public class SalesMetricDTO
{
    public decimal Total { get; set; }
    public int Count { get; set; }
    public string Date { get; set; } = string.Empty;
}