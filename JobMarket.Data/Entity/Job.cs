using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobMarket.Data.Entity
{
    [Table("Job")]
    public class Job
    {
        [Key]
        public int ID {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        [DefaultValue(0.0)]
        [Range(0.00, 999999.99, ErrorMessage = "Budget must be between 0.00 and 999999.99")]
        public double Budget { get; set; }
        [Column(TypeName = "text")]
        public string? Description { get; set; }
        public Nullable<int> AcceptedById { get;set;  }
        [ForeignKey("AcceptedById")]
        public Customer? AcceptedBy { get; set; }
    }
}
