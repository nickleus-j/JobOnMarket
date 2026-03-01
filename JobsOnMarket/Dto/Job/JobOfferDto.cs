using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JobMarket.Data.Entity;

namespace JobsOnMarket.Dto.Job;

public class JobOfferDto
{
    public int ID { get; set; }
    [DefaultValue(1.0)]
    [Range(0.00, 999999.99, ErrorMessage = "Price must be between 0.00 and 999999.99")]
    public double Price { get; set; }
    public string CurrencyCode { get; set; }
    public int JobId { get; set; }
}