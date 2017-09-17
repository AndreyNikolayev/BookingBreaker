namespace BookingDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlacesStyle : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ShowTimePlaces");
            CreateTable(
                "dbo.ShowTimePlaceStyles",
                c => new
                    {
                        ShowTimePlaceId = c.Int(nullable: false, identity: true),
                        Left = c.Double(nullable: false),
                        Top = c.Double(nullable: false),
                        Width = c.Double(nullable: false),
                        Height = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ShowTimePlaceId);
            
            AlterColumn("dbo.ShowTimePlaces", "ShowTimePlaceId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ShowTimePlaces", "ShowTimePlaceId");
            CreateIndex("dbo.ShowTimePlaces", "ShowTimePlaceId");
            AddForeignKey("dbo.ShowTimePlaces", "ShowTimePlaceId", "dbo.ShowTimePlaceStyles", "ShowTimePlaceId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShowTimePlaces", "ShowTimePlaceId", "dbo.ShowTimePlaceStyles");
            DropIndex("dbo.ShowTimePlaces", new[] { "ShowTimePlaceId" });
            DropPrimaryKey("dbo.ShowTimePlaces");
            AlterColumn("dbo.ShowTimePlaces", "ShowTimePlaceId", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.ShowTimePlaceStyles");
            AddPrimaryKey("dbo.ShowTimePlaces", "ShowTimePlaceId");
        }
    }
}
