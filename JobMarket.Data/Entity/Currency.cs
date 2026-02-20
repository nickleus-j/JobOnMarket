using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobMarket.Data.Entity
{
    [Table("Currency")]
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
