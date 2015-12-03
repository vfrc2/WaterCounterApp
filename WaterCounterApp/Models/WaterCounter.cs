using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WaterCounterApp.Models
{
    public class WaterCounter
    {
        [Key]
        public virtual int WaterCounterId { get; set; }

        [Required]
        public virtual string SerialNum { get; set; }


        public virtual int Readings { get; set; }

    }
}