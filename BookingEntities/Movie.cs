using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingDataAccess
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public virtual ICollection<ShowTime> Showtimes { get; set; }

        public virtual ICollection<ShowDuration> Showdurations { get; set; }

        public virtual ICollection<LocalMovieIdentity> LocalMovieIdentities { get; set; }

        public string Title { get; set; }

        public int? Duration { get; set; }
    }
}
