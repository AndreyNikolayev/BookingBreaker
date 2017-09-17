using BookingDataAccess;
using Microsoft.AspNet.Identity;
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
    [Authorize]
    [RoutePrefix("api/subscribe")]
    public class SubscribeStartSalesController : ApiController
    {
        BookingBreakerContext db = new BookingBreakerContext(false);

        [HttpPost]
        public IHttpActionResult CreateSubscription([FromBody]int movieId)
        {
            try
            {
                var userId = User.Identity.GetUserId();

                var startSubscription = db.StartSalesSubscriptions.FirstOrDefault(p => p.MovieId == movieId && p.UserId == userId);

                if (startSubscription == null)
                {
                    var subscription = new StartSalesSubscription
                    {
                        MovieId = movieId,
                        UserId = userId
                    };

                    db.StartSalesSubscriptions.Add(subscription);
                    db.SaveChanges();
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return new ExceptionResult(ex, this);
            }
        }
    }
}
