using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobMarket.Data.Entity;

public class JobDoneReport
{
    [Key]
    public int ID { get; set; }
    [DefaultValue(0.0)]
    [Range(0, 5, ErrorMessage = "Budget must be between 0 and 10")]
    public short Rating { get; set; }
    [Required]
    [Column(TypeName = "text")]
    public string Description { get; set; }
    public DateTime DateReported { get; set; }
    
    public int JobOfferId { get; set; }
    [ForeignKey("JobOfferId")]
    public JobOffer? OfferCompleted { get; set; }
}