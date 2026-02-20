using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobMarket.Data.Entity
{
    [Table("JobOffer")]
    public class JobOffer
    {
        [Key]
        public int ID { get; set; }
        [DefaultValue(1.0)]
        [Range(0.00, 999999.99, ErrorMessage = "Price must be between 0.00 and 999999.99")]
        public double Price { get; set; }
        [DefaultValue(1)]
        public Nullable<int> PriceCurrencyId { get; set; }
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        public Job? OfferedJob { get; set; }
        public Nullable<int> OfferedByContractorId { get; set; }
        [ForeignKey("OfferedByContractorId")]
        public Contractor? OfferedByContractor { get; set; }
        [ForeignKey("PriceCurrencyId")]
        public Currency? PriceCurrency { get; set; }
    }
}
