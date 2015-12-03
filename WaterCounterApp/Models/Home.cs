using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Home
    {
        [Key]
        public virtual int HomeId { get; set; }

        [Required]
        public virtual string Address { get; set; }

        public virtual List<WaterCounter> Counters { get; set; }
    }
}