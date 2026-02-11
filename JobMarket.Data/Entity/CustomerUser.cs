using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobMarket.Data.Entity
{
    [Table("CustomerUser")]
    public class CustomerUser
    {
        [Key]
        public int ID { get; set; }
        public Nullable<int> CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer? AssociatedCustomer { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
    }
}
