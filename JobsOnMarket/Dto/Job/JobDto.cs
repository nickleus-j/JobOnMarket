using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsOnMarket.Dto.Job;

public class JobDto
{
    public int ID {  get; set; }
    public DateTime StartDate { get; set; }
    public DateTime DueDate { get; set; }
    [DefaultValue(0.0)]
    [Range(0.00, 999999.99, ErrorMessage = "Budget must be between 0.00 and 999999.99")]
    public double Budget { get; set; }
    public string CurrencyCode { get; set; }
    public string? Description { get; set; }
}