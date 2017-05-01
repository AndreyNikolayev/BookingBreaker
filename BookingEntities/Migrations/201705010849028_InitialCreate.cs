namespace BookingDataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cinemas",
                c => new
                    {
                        CinemaId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Website = c.String(),
                        Address = c.String(),
                        Contacts = c.String(),
                    })
                .PrimaryKey(t => t.CinemaId);
            
            CreateTable(
                "dbo.LocalMovieIdentities",
                c => new
                    {
                        LocalMovieIdentityId = c.Int(nullable: false, identity: true),
                        CinemaId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        LocalIdentifier = c.String(),
                    })
                .PrimaryKey(t => t.LocalMovieIdentityId)
                .ForeignKey("dbo.Cinemas", t => t.CinemaId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.CinemaId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Duration = c.Int(),
                    })
                .PrimaryKey(t => t.MovieId);
            
            CreateTable(
                "dbo.ShowDurations",
                c => new
                    {
                        ShowDurationId = c.Int(nullable: false, identity: true),
                        CinemaId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        StartShowDate = c.DateTime(nullable: false),
                        EndShowDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ShowDurationId)
                .ForeignKey("dbo.Cinemas", t => t.CinemaId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.CinemaId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.ShowTimes",
                c => new
                    {
                        ShowTimeId = c.Int(nullable: false, identity: true),
                        Link = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        CinemaId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShowTimeId)
                .ForeignKey("dbo.Cinemas", t => t.CinemaId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.CinemaId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.ShowTimePlaces",
                c => new
                    {
                        ShowTimePlaceId = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        PlaceNumber = c.Int(nullable: false),
                        ShowTimeId = c.Int(nullable: false),
                        PlaceAccess = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShowTimePlaceId)
                .ForeignKey("dbo.ShowTimes", t => t.ShowTimeId, cascadeDelete: true)
                .Index(t => t.ShowTimeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalMovieIdentities", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.ShowTimePlaces", "ShowTimeId", "dbo.ShowTimes");
            DropForeignKey("dbo.ShowTimes", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.ShowTimes", "CinemaId", "dbo.Cinemas");
            DropForeignKey("dbo.ShowDurations", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.ShowDurations", "CinemaId", "dbo.Cinemas");
            DropForeignKey("dbo.LocalMovieIdentities", "CinemaId", "dbo.Cinemas");
            DropIndex("dbo.ShowTimePlaces", new[] { "ShowTimeId" });
            DropIndex("dbo.ShowTimes", new[] { "MovieId" });
            DropIndex("dbo.ShowTimes", new[] { "CinemaId" });
            DropIndex("dbo.ShowDurations", new[] { "MovieId" });
            DropIndex("dbo.ShowDurations", new[] { "CinemaId" });
            DropIndex("dbo.LocalMovieIdentities", new[] { "MovieId" });
            DropIndex("dbo.LocalMovieIdentities", new[] { "CinemaId" });
            DropTable("dbo.ShowTimePlaces");
            DropTable("dbo.ShowTimes");
            DropTable("dbo.ShowDurations");
            DropTable("dbo.Movies");
            DropTable("dbo.LocalMovieIdentities");
            DropTable("dbo.Cinemas");
        }
    }
}
