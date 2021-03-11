using CinemaC.Models.Domain;
using CinemaC.Models.Tickets;

namespace CinemaC.Interfaces
{
    public interface ITicketService
    {
        MovieListItem[] GetFullMoviesInfo();
        Movie GetMovieById(int id);
        Movie[] GetAllMovies();
        bool UpdateMovie(Movie movie);

        Hall GetHallById(int id);
        Hall[] GetAllHalls();
        bool UpdateHall(Hall movie);

        TimeSlot GetTimeSlotById(int id);
        TimeSlot[] GetAllTimeSlots();
        bool UpdateTimeSlot(TimeSlot movie);

        TimeSlotTag[] GetTimeSlotTagsByMovieId(int movieId);

        bool CreateMovie(Movie newMovie);
        bool CreateTimeSlot(TimeSlot newTimeSlot);
        bool CreateHall(Hall newHall);

        bool AddRequestedSeatsToTimeSlot(SeatsProcessRequest request);
    }
}