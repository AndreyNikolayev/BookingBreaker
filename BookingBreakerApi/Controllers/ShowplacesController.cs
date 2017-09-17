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
    public class ShowplacesController : ApiController
    {
        BookingBreakerContext db = new BookingBreakerContext(false);

        public IHttpActionResult Get(int id)
        {
            var showplaces = db.ShowTimePlaces
                .Where(p => p.ShowTimeId == id)
                .Include(p => p.ShowTimePlaceStyle).ToList();

            return Json(showplaces);
        }
    }
}
