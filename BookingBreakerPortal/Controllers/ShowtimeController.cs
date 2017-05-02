using BookingDataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace BookingBreakerPortal.Controllers
{
    public class ShowtimeController : Controller
    {
        BookingBreakerContext db = new BookingBreakerContext(false);

        // GET: Showtime
        public ActionResult Index()
        {
            var model = db.ShowTimes.Include(p => p.Movie).Include(p => p.CinemaHall).Include(p => p.CinemaHall.Cinema).OrderBy(p => p.StartTime).ToList();
            return View(model);
        }

        public ActionResult GetShowPlacesForShowPlace(int showtimeId)
        {
            var places = db.ShowTimePlaces.Where(p => p.ShowTimeId == showtimeId).ToList();

            return new JsonResult { Data = places, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}