﻿using CinemaC.Models.Domain;

namespace CinemaC.Interfaces
{
    public interface ITicketService
    {
        Movie GetMovieById(int id);
        Movie[] GetAllMovies();
        Hall GetHallById(int id);
        Hall[] GetAllHalls();
        TimeSlot GetTimeSlotById(int id);
        TimeSlot[] GetAllTimeSlots();
        bool UpdateMovie(Movie movie);
        bool UpdateHall(Hall movie);
        bool UpdateTimeSlot(TimeSlot movie);
    }
}