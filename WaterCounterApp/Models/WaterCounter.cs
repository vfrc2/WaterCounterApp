using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace WaterCounterApp.Models
{
    public class WaterCounter
    {
        [Key]
        public virtual int WaterCounterId { get; set; }

        [Required]
        public virtual string SerialNum { get; set; }

        [Required]
        public virtual int Readings { get; set; }

        public virtual Home Home { get; set; }

        //public int Home_HomeId { get; set; }

    }

    //internal class WaterCounterConfig: EntityTypeConfiguration<WaterCounter>
    //{
    //    public WaterCounterConfig()
    //    {
    //        ToTable("dbo.Homes");

    //        HasRequired(t => t.Home).WithMany(c => c.Counters).HasForeignKey(t => t.Home_HomeId).WillCascadeOnDelete(true);
    //    }
    //}
}