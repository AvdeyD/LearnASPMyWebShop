
namespace CinemaC.Models.Tickets
{
    public class HallInfo
    {
        public int RowsCount { get; set; }
        public int ColumnsCount { get; set; }
        public decimal TicketCost { get; set; }

        public int CurrentTimeSlotId { get; set; }
        public TimeSlotSeatRequest[] RequestedSeats { get; set; }

    }
}