namespace BookingDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLocalMovieLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LocalMovieIdentities", "LocalMovieLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LocalMovieIdentities", "LocalMovieLink");
        }
    }
}
