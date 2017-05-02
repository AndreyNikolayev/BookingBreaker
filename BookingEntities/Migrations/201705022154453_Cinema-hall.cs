namespace BookingDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cinemahall : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShowTimes", "CinemaId", "dbo.Cinemas");
            DropIndex("dbo.ShowTimes", new[] { "CinemaId" });
            CreateTable(
                "dbo.CinemaHalls",
                c => new
                    {
                        CinemaHallId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        PlacesRepresentationTech = c.Int(nullable: false),
                        HallSchema = c.String(),
                        LocalCinemaHallId = c.Int(nullable: false),
                        HorizontalHallLayout = c.Int(nullable: false),
                        VerticalHallLayout = c.Int(nullable: false),
                        CinemaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CinemaHallId)
                .ForeignKey("dbo.Cinemas", t => t.CinemaId, cascadeDelete: true)
                .Index(t => t.CinemaId);
            
            AddColumn("dbo.ShowTimes", "CinemaHallId", c => c.Int(nullable: false));
            AddColumn("dbo.ShowTimes", "Technology", c => c.Int(nullable: false));
            CreateIndex("dbo.ShowTimes", "CinemaHallId");
            AddForeignKey("dbo.ShowTimes", "CinemaHallId", "dbo.CinemaHalls", "CinemaHallId", cascadeDelete: true);
            DropColumn("dbo.ShowTimes", "CinemaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShowTimes", "CinemaId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CinemaHalls", "CinemaId", "dbo.Cinemas");
            DropForeignKey("dbo.ShowTimes", "CinemaHallId", "dbo.CinemaHalls");
            DropIndex("dbo.ShowTimes", new[] { "CinemaHallId" });
            DropIndex("dbo.CinemaHalls", new[] { "CinemaId" });
            DropColumn("dbo.ShowTimes", "Technology");
            DropColumn("dbo.ShowTimes", "CinemaHallId");
            DropTable("dbo.CinemaHalls");
            CreateIndex("dbo.ShowTimes", "CinemaId");
            AddForeignKey("dbo.ShowTimes", "CinemaId", "dbo.Cinemas", "CinemaId", cascadeDelete: true);
        }
    }
}
