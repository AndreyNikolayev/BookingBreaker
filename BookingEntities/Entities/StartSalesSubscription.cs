using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingDataAccess
{
    public class StartSalesSubscription
    {
        [Key]
        public int StartSalesSubscriptionId { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsUserNotified { get; set; }
    }
}
