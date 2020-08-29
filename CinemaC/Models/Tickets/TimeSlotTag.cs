using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaC.Models.Tickets
{
    public class TimeSlotTag
    {
        public int TimeSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public decimal Cost { get; set; }

    }
}