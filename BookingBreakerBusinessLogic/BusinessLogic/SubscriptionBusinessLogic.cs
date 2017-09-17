using System.Threading.Tasks;
using NLog;
using BookingDataAccess;
using System.Data.Entity;
using System.Linq;
using BookingBreakerBusinessLogic.Services;
using System.Collections.Generic;
using System;

namespace BookingBreakerBusinessLogic
{
    public class SubscriptionBusinessLogic
    {
        private static Logger _logger = LogManager.GetLogger("BookingBreaker");


        public static void CheckStartSubscriptionsSend()
        {
            try
            {
                using (BookingBreakerContext db = new BookingBreakerContext())
                {
                    var runningSalesMoviesIds = db.ShowTimes.Select(p => p.MovieId).Distinct().ToList();
                    var startedSalesSubscriptions = db.StartSalesSubscriptions.Where(p => p.IsUserNotified == false && runningSalesMoviesIds.Contains(p.MovieId))
                        .Include(p => p.Movie).Include(p => p.User).ToList();

                    if (startedSalesSubscriptions.Count == 0)
                    {
                        _logger.Info("No subscriptions up to date");
                        return;
                    }

                    Parallel.ForEach(startedSalesSubscriptions, subscription =>
                    {
                        var isSent = MailService.DefaultInstance.SendEmail(subscription.User.Email, "Старт продаж билетов на " + subscription.Movie.Title,
                            "<h1>Стартовали продажи билетов на " + subscription.Movie.Title + "! Не пропусти шанс занять лучшие места!</h1>");

                        subscription.IsUserNotified = isSent;
                        _logger.Info("Working with subscription - Movie: " + subscription.Movie.Title + 
                            ", UserEmail: " + subscription.User.Email + ", IsSent: " + isSent);
                    });

                    _logger.Info("Try save subscription status");
                    db.SaveChanges();
                    _logger.Info("Save subscription status changes");
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }
    }
}
