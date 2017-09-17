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
                .OrderBy(p => p.StartTime).FirstOrDefault();

            showtime.CinemaHall.Cinema.CinemaHalls = null;
            showtime.Movie.Showtimes = null;

            return Json(showtime);
        }

        public IHttpActionResult Get()
        {
            var showtimes = db.ShowTimes
                .Where(p => p.StartTime > DateTime.Now)
                .Include(p => p.Movie)
                .Include(p => p.CinemaHall)
                .Include(p => p.CinemaHall.Cinema)
                .OrderBy(p => p.StartTime).ToList();

            showtimes.ForEach(p => {
                p.CinemaHall.Cinema.CinemaHalls = null;
                p.Movie.Showtimes = null;
            });

            return Json(showtimes);
        }
    }
}
