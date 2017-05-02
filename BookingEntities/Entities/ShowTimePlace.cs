using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingDataAccess
{
    public class ShowTimePlace
    {
        [Key]
        public int ShowTimePlaceId { get; set; }

        public int Row { get; set; }

        public int PlaceNumber { get; set; }

        [ForeignKey("ShowTime")]
        public int ShowTimeId { get; set; }

        public ShowTime ShowTime {get; set;}

        public PlaceAccessEnum PlaceAccess { get; set; }


    }
}
