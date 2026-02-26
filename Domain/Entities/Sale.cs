using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MDashboard.Domain.Entities;

public class Sale
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public decimal Value { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public string? Category { get; set; }
}