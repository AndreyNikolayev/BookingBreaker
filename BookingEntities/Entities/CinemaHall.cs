using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingDataAccess
{
    public class CinemaHall
    {
        [Key]
        public int CinemaHallId { get; set; }

        public string Title { get; set; }

        public PlacesRepresentationTechEnum PlacesRepresentationTech { get; set; }

        public string HallSchema { get; set; }

        public int LocalCinemaHallId { get; set; }

        public HorizontalHallLayoutEnum HorizontalHallLayout { get; set; }

        public VerticalHallLayoutEnum VerticalHallLayout { get; set; }

        [ForeignKey("Cinema")]
        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; }


    }
}
