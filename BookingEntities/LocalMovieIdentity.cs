using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingDataAccess
{
    public class LocalMovieIdentity
    {
        [Key]
        public int LocalMovieIdentityId { get; set; }

        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }

        public Movie Movie { get; set; }

        public string LocalMovieLink { get; set; }

        public string LocalIdentifier { get; set; }

    }
}
