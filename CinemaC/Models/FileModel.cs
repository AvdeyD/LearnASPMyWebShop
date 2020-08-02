using CinemaC.Models.Domain;

namespace CinemaC.Models
{
    public class FileModel
    {
        public Movie[] Movies { get; set; }
        public Hall[] Halls { get; set; }
        public TimeSlot[] TimeSlots { get; set; }
    }
}