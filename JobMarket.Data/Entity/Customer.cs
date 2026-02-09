using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobMarket.Data.Entity
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int ID {  get; set; }
        [Required]
        [StringLength(80)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(80)]
        public string LastName { get; set; }
    }
}
