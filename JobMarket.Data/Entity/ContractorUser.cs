using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobMarket.Data.Entity
{
    public class ContractorUser
    {
        [Key]
        public int ID { get; set; }
        public Nullable<int> ContractorId { get; set; }
        [ForeignKey("ContractorId")]
        public Contractor? AssociatedContractor { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
    }
}
