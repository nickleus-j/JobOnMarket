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
        public DateTime DuetDate { get; set; }
        [DefaultValue(0.0)]
        public double Budget { get; set; }
        [Column(TypeName = "text")]
        public string? Description { get; set; }
        public Nullable<int> AcceptedById { get;set;  }
        [ForeignKey("AcceptedById")]
        public Customer? AcceptedBy { get; set; }
    }
}
