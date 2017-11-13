using BookingDataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;

namespace BookingBreakerApi.Controllers
{
    public class ShowtimesController : ApiController
    {
        BookingBreakerContext db = new BookingBreakerContext(false);

        public IHttpActionResult Get(int id)
        {
            var showtime = db.ShowTimes
                .Where(p => p.ShowTimeId == id)
                .Include(p => p.Movie)
                .Include(p => p.CinemaHall)
                .Include(p => p.CinemaHall.Cinema)
                .FirstOrDefault();

            showtime.CinemaHall.Cinema.CinemaHalls = null;
            showtime.Movie.Showtimes = null;

            return Json(showtime);
        }

        public IHttpActionResult Get()
        {
            var currentDate = DateTime.Now.Date;
            var nextDate = DateTime.Now.AddDays(1).Date;
            var cinemaWithshowtimes = db.ShowTimes
                .Where(p => p.StartTime > currentDate && p.StartTime < nextDate)
                .Include(p => p.Movie)
                .Include(p => p.CinemaHall)
                .Include(p => p.CinemaHall.Cinema)
                .OrderBy(p => p.StartTime)
                .ToList()
                .GroupBy(p => p.CinemaHall.Cinema)
                .Select(p => new
                {
                    Cinema = p.Key,
                    Movies = p.GroupBy(s => s.Movie)
                    .Select(s => s.Key).ToList()
                })
                .ToList();

            cinemaWithshowtimes.ForEach(p =>
            {
                p.Cinema.CinemaHalls = null;
                p.Movies.ForEach(m =>
                {
                    foreach(var s in m.Showtimes)
                    {
                        s.Movie = null;
                    }
                });
            });

            return Json(cinemaWithshowtimes);
        }
    }
}
