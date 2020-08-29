using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CinemaC.Models.Domain;

namespace CinemaC.Models.Tickets
{
    public class MovieListItem
    {
        public Movie Movie { get; set; }
        public TimeSlotTag[] AvailableTimeSlots { get; set; }

    }
}