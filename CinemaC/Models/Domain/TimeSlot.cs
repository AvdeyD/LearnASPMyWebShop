using System;

namespace CinemaC.Models.Domain
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public DateTime StarTime { get; set; }
        public decimal Cost { get; set; }
        public Movie Movie { get; set; }
        public Hall Hall { get; set; }
        public Format Format { get; set; }
    }
}