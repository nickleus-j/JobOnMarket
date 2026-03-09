using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobsOnMarket.Dto.Job;

public class JobDoneDto
{
  public int ID { get; set; }
  [DefaultValue(0.0)]
  [Range(0, 5, ErrorMessage = "Budget must be between 0 and 10")]
  public short Rating { get; set; }
  [Required]
  public string Description { get; set; }
  public DateTime DateReported { get; set; }
  public double price { get; set; }
  public string CurrencyCode { get; set; }
  public int JobId { get; set; }
  public string CustomerName { get; set; }
  public string ContractorName { get; set; }
}