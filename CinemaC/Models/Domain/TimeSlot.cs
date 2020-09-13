using System;
using CinemaC.Models.Tickets;

namespace CinemaC.Models.Domain
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public DateTime StarTime { get; set; }
        public decimal Cost { get; set; }
        public int MovieId { get; set; }
        public int HallId { get; set; }
        public Format Format { get; set; }
        public TimeSlotSeatRequest[] RequestedSeats {get; set; }
    }
}