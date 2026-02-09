using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JobMarket.Data.Entity
{
    [Table("Contractor")]
    public class Contractor
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(60)]
        public string Name { get; set; }
        [DefaultValue(0)]
        public int Rating {  get; set; }
    }
}
