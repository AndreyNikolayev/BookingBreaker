namespace BookingDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isSentSub : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StartSalesSubscriptions", "IsUserNotified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StartSalesSubscriptions", "IsUserNotified");
        }
    }
}
