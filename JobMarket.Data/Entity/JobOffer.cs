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
        public double Price { get; set; }
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        public Job? OfferedJob { get; set; }
        public Nullable<int> OfferedById { get; set; }
        [ForeignKey("OfferedById")]
        public Customer? OfferedBy { get; set; }
    }
}
