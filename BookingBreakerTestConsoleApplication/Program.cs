using BookingBreakerBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingBreakerTestConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            PlanetaParcingBusinessLogic.ExecuteParcing().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
