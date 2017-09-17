using BookingBreakerBusinessLogic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookingBreakerTestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // PlanetaParcingBusinessLogic.ExecuteMovieParcing().ConfigureAwait(false).GetAwaiter().GetResult();
            // PlanetaParcingBusinessLogic.ExecuteShowTimesParcing().ConfigureAwait(false).GetAwaiter().GetResult();

            SubscriptionBusinessLogic.CheckStartSubscriptionsSend();
        }
    }
}
