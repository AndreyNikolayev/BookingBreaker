using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingDataAccess
{
    public class Cinema
    {
        [Key]
        public int CinemaId { get; set; }

        public string Title { get; set; }

        public virtual ICollection<CinemaHall> CinemaHalls { get; set; }

        public virtual ICollection<ShowDuration> Showdurations { get; set; }

        public virtual ICollection<LocalMovieIdentity> LocalMovieIdentities { get; set; }

        public string Website { get; set; }

        public string Address { get; set; }

        public string Contacts { get; set; }
    }
}
