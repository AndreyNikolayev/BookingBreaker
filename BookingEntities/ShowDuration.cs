using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingDataAccess
{
    public class ShowDuration
    {
        [Key]
        public int ShowDurationId { get; set; }

        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public DateTime StartShowDate { get; set; }

        public DateTime EndShowDate { get; set; }

    }
}
