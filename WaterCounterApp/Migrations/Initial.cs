namespace WaterCounterApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Homes",
                c => new
                    {
                        HomeId = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.HomeId);
            
            CreateTable(
                "dbo.WaterCounters",
                c => new
                    {
                        WaterCounterId = c.Int(nullable: false, identity: true),
                        SerialNum = c.String(nullable: false),
                        Readings = c.Int(nullable: false),
                        Home_HomeId = c.Int(),
                    })
                .PrimaryKey(t => t.WaterCounterId)
                .ForeignKey("dbo.Homes", t => t.Home_HomeId)
                .Index(t => t.Home_HomeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WaterCounters", "Home_HomeId", "dbo.Homes");
            DropIndex("dbo.WaterCounters", new[] { "Home_HomeId" });
            DropTable("dbo.WaterCounters");
            DropTable("dbo.Homes");
        }
    }
}
