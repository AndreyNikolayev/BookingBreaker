using BookingDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingBreakerPortal.Controllers
{
    public class MovieController : Controller
    {
        BookingBreakerContext db = new BookingBreakerContext();
        // GET: Movie
        public ActionResult Index()
        {
            
            return View(db.Movies.ToList());
        }
    }
}