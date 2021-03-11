using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AutoMapper;
using CinemaC.Interfaces;
using CinemaC.Models.Domain;
using CinemaC.Models.Tickets;
using CinemaC.Utils;

namespace CinemaC.Services
{
    public class SqlTicketService:ITicketService
    {
        private readonly SqlDatabaseUtil _sqlDatabaseUtil;

        public SqlTicketService(IMapper mapper)
        {
            _sqlDatabaseUtil = new SqlDatabaseUtil(mapper);
        }

        public MovieListItem[] GetFullMoviesInfo()
        {
            var allMovies = GetAllMovies();
            var resultModel = new List<MovieListItem>();
            foreach (var movie in allMovies)
            {
                resultModel.Add(
                    new MovieListItem
                    {
                        Movie = movie,
                        AvailableTimeSlots = GetTimeSlotTagsByMovieId(movie.Id)
                    });
            }

            return resultModel.ToArray();
        }

        public Movie GetMovieById(int id)
        {
            return _sqlDatabaseUtil.Select<Movie>("select * from movies where id = @id",
                new SqlParameter("@id", id)).FirstOrDefault();
        }

        public Movie[] GetAllMovies()
        {
            return _sqlDatabaseUtil.Select<Movie>("select*from movies").ToArray();
        }

        public bool UpdateMovie(Movie movie)
        {
            return _sqlDatabaseUtil.Execute("update movies set title = @title where id = @id",
                new SqlParameter("@id", movie.Id),
                new SqlParameter("@title", movie.Title));
        }

        public Hall GetHallById(int id)
        {
            return _sqlDatabaseUtil.Select<Hall>("select * from halls where id = @id",
                    new SqlParameter("@id", id)).FirstOrDefault();
        }

        public bool RemoveMovie(int id)
        {
            return _sqlDatabaseUtil.Execute("delete from movies where id = @id", new SqlParameter("id", id));
        }

        public Hall[] GetAllHalls()
        {
            return _sqlDatabaseUtil.Select<Hall>("select*from halls").ToArray();
        }

        public bool UpdateHall(Hall movie)
        {
            return _sqlDatabaseUtil.Execute("update hall set name = @name, places = @places  where id = @id",
                new SqlParameter("@id", movie.Id), new SqlParameter("@name", movie.Name), new SqlParameter("@places", movie.Places));
        }

        public TimeSlot GetTimeSlotById(int id)
        {
            return _sqlDatabaseUtil.Select<TimeSlot>("select * from TimeSlots where id = @id",
                new SqlParameter("@id", id)).FirstOrDefault();
        }

        public TimeSlot[] GetTimeSlotByMovieId(int id)
        {
            throw new NotImplementedException();
        }

        public TimeSlot[] GetAllTimeSlots()
        {
            return _sqlDatabaseUtil.Select<TimeSlot>("select*from TimeSlots").ToArray();
        }

        public bool UpdateTimeSlot(TimeSlot movie)
        {
            return _sqlDatabaseUtil.Execute("update timeslots set Cost = @Cost, StarTime = @StarTime, Format = @Format, HallId = @HallId, RequestedSeats = @RequestedSeats, MovieId = @MovieId  where id = @id",
                new SqlParameter("@id", movie.Id),
                new SqlParameter("@cost", movie.Cost),
                new SqlParameter("@StarTime", movie.StarTime),
                new SqlParameter("@Format", movie.Format.ToString()),
                new SqlParameter("@HallId", movie.HallId),
                new SqlParameter("@RequestedSeats", movie.RequestedSeats),
                new SqlParameter("@MovieId", movie.MovieId));
        }

        public TimeSlotTag[] GetTimeSlotTagsByMovieId(int movieId)
        {
            var timeSlots = GetTimeSlotByMovieId(movieId);
            var resultModel = new List<TimeSlotTag>();
            foreach (var timeSlot in timeSlots)
            {
                resultModel.Add(new TimeSlotTag
                {
                    TimeSlotId = timeSlot.Id,
                    StartTime = timeSlot.StarTime,
                    Cost = timeSlot.Cost
                });
            }

            return resultModel.ToArray();
        }

        public bool CreateMovie(Movie newMovie)
        {
            throw new NotImplementedException();
        }

        public bool CreateTimeSlot(TimeSlot newTimeSlot)
        {
            throw new NotImplementedException();
        }

        public bool CreateHall(Hall newHall)
        {
            throw new NotImplementedException();
        }

        public bool AddRequestedSeatsToTimeSlot(SeatsProcessRequest request)
        {
            throw new NotImplementedException();
        }
    }
}