using CinemaC.Models.Domain;

namespace CinemaC.Models.Tickets
{
    public enum RequestStatus
    {
        Bye,
        Reserve
    }

    public class SeatsProcessRequest
    {
        public int TimeSlotId { get; set; }
        public SeatsRequest SeatsRequest { get; set; }
        public RequestStatus SelectedStatus { get; set; }
    }

    public class SeatsRequest
    {
        public SelectedSeat[] AddedSeats { get; set; }
        public decimal Sum { get; set; }
    }

    public class SelectedSeat
    {
        public int Row { get; set; }
        public int Seat { get; set; }
    }

    public class TimeSlotSeatRequest
    {
        public int Row { get; set; }
        public int Seat { get; set; }
        public RequestStatus Status { get; set; }
    }
}
