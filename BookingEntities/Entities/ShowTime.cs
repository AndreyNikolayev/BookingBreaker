using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingDataAccess
{
    public class ShowTime
    {
        [Key]
        public int ShowTimeId { get; set; }

        public string Link { get; set; }

        public DateTime StartTime { get; set; }

        public virtual ICollection<ShowTimePlace> ShowTimePlaces { get; set; }

        [ForeignKey("CinemaHall")]
        public int CinemaHallId { get; set; }

        public CinemaHall CinemaHall { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public TechnologyEnum Technology { get; set; }
    }
}
