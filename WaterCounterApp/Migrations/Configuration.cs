namespace WebApplication2.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication2.Models.WaterCounterDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebApplication2.Models.WaterCountersDbContext";
        }

        protected override void Seed(WebApplication2.Models.WaterCounterDbContext context)
        {

            var home =  new Models.Home() { Address = "Moscow, Strret1, home=1", HomeId = 1 };
            home.Counters = new List<Models.WaterCounter>(new Models.WaterCounter[] {
                new Models.WaterCounter() { SerialNum="x0001", WaterCounterId=1, Readings=0 }
               });
            
            context.Homes.AddOrUpdate(home);

            home = new Models.Home() { Address = "Moscow, Strret-45, home=43", HomeId = 2 };
            home.Counters = new List<Models.WaterCounter>(new Models.WaterCounter[] {
                new Models.WaterCounter() { SerialNum="x1321", WaterCounterId=3, Readings=0 },
                new Models.WaterCounter() { SerialNum="x1322", WaterCounterId=4, Readings=0 }
               });

            context.Homes.AddOrUpdate(home);

            home = new Models.Home() { Address = "Moscow, Strret-kidjlldl, home=145", HomeId = 3 };
            home.Counters = new List<Models.WaterCounter>(new Models.WaterCounter[] {
                new Models.WaterCounter() { SerialNum="sn456701", WaterCounterId=6, Readings=0 }
               });

            context.Homes.AddOrUpdate(home);
        }
    }
}
