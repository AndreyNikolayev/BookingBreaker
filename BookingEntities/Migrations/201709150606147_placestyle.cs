namespace BookingDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class placestyle : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShowTimePlaces", "ShowTimePlaceId", "dbo.ShowTimePlaceStyles");
            DropIndex("dbo.ShowTimePlaces", new[] { "ShowTimePlaceId" });
            DropPrimaryKey("dbo.ShowTimePlaces");
            DropPrimaryKey("dbo.ShowTimePlaceStyles");
            DropColumn("dbo.ShowTimePlaceStyles", "ShowTimePlaceId");
            AddColumn("dbo.ShowTimePlaces", "ShowTimePlaceStyleId", c => c.Int(nullable: false));
            AddColumn("dbo.ShowTimePlaceStyles", "ShowTimePlaceStyleId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ShowTimePlaces", "ShowTimePlaceId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ShowTimePlaces", "ShowTimePlaceId");
            AddPrimaryKey("dbo.ShowTimePlaceStyles", "ShowTimePlaceStyleId");
            CreateIndex("dbo.ShowTimePlaces", "ShowTimePlaceStyleId");
            AddForeignKey("dbo.ShowTimePlaces", "ShowTimePlaceStyleId", "dbo.ShowTimePlaceStyles", "ShowTimePlaceStyleId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShowTimePlaceStyles", "ShowTimePlaceId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.ShowTimePlaces", "ShowTimePlaceStyleId", "dbo.ShowTimePlaceStyles");
            DropIndex("dbo.ShowTimePlaces", new[] { "ShowTimePlaceStyleId" });
            DropPrimaryKey("dbo.ShowTimePlaceStyles");
            DropPrimaryKey("dbo.ShowTimePlaces");
            AlterColumn("dbo.ShowTimePlaces", "ShowTimePlaceId", c => c.Int(nullable: false));
            DropColumn("dbo.ShowTimePlaceStyles", "ShowTimePlaceStyleId");
            DropColumn("dbo.ShowTimePlaces", "ShowTimePlaceStyleId");
            AddPrimaryKey("dbo.ShowTimePlaceStyles", "ShowTimePlaceId");
            AddPrimaryKey("dbo.ShowTimePlaces", "ShowTimePlaceId");
            CreateIndex("dbo.ShowTimePlaces", "ShowTimePlaceId");
            AddForeignKey("dbo.ShowTimePlaces", "ShowTimePlaceId", "dbo.ShowTimePlaceStyles", "ShowTimePlaceId");
        }
    }
}
