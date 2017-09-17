using BookingDataAccess;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingDataAccess
{
    public class BookingBreakerContext : IdentityDbContext<ApplicationUser>
    {
        public static BookingBreakerContext Create()
        {
            return new BookingBreakerContext();
        }

        public BookingBreakerContext() : base("DefaultConnection")
        {

        }

        public BookingBreakerContext(bool isLazy) : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = isLazy;
        }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<LocalMovieIdentity> LocalMovieIdentities { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<ShowDuration> ShowDurations { get; set; }

        public DbSet<ShowTime> ShowTimes { get; set; }

        public DbSet<ShowTimePlace> ShowTimePlaces { get; set; }

        public DbSet<ShowTimePlaceStyle> ShowTimePlaceStyles { get; set; }

        public DbSet<CinemaHall> CinemaHalls { get; set; }

        public DbSet<StartSalesSubscription> StartSalesSubscriptions { get; set; }
    }
}
