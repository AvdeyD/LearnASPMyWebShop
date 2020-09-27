using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using CinemaC.DataBaseLayer;
using CinemaC.Interfaces;
using CinemaC.Models.Domain;
using CinemaC.Models.Tickets;

namespace CinemaC.Services
{
    public class EntityTicketsServise: ITicketService
    {
        public MovieListItem[] GetFullMoviesInfo()
        {
            var model = new List<MovieListItem>();
            using (var context = new CinemaContext())
            {
                foreach (var movie in context.Movies.Where(x => !x.IsDeleted).ToArray())
                {
                    var movieListItem = new MovieListItem
                    {
                        Movie = movie,
                        AvailableTimeSlots = context.TimeSlots
                            .Where(x => x.MovieId == movie.Id)
                            .Select(x => new TimeSlotTag
                            {
                                Cost = x.Cost,
                                StartTime = x.StarTime,
                                TimeSlotId = x.Id
                            })
                            .ToArray()
                    };
                    model.Add(movieListItem);
                }
            }

            return model.ToArray();
        }

        public Movie GetMovieById(int id)
        {
            using (var context = new CinemaContext())
            {
                return context.Movies.Where(x => !x.IsDeleted).First(x => x.Id==id);
            }
        }

        public Movie[] GetAllMovies()
        {
            using (var context = new CinemaContext())
            {
                return context.Movies.Where(x=>!x.IsDeleted).ToArray();
            }
        }

        public bool UpdateMovie(Movie movie)
        {
            using (var context = new CinemaContext())
            {
                context.Movies.AddOrUpdate(movie);
                context.SaveChanges();
            }

            return true;
        }

        public bool RemoveMovie(int id)
        {
            using (var context = new CinemaContext())
            {
                var movie = context.Movies.First(x => x.Id == id);
                movie.IsDeleted = true;
                context.SaveChanges();
            }

            return true;
        }

        public Hall GetHallById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Hall[] GetAllHalls()
        {
            using (var context = new CinemaContext())
            {
               return  context.Halls.ToArray();
            }
        }

        public bool UpdateHall(Hall movie)
        {
            throw new System.NotImplementedException();
        }

        public TimeSlot GetTimeSlotById(int id)
        {
            throw new System.NotImplementedException();
        }

        public TimeSlot[] GetTimeSlotByMovieId(int id)
        {
            using (var context = new CinemaContext())
            {
                return context.TimeSlots.Where(x => x.MovieId == id).ToArray();
            }
        }

        public TimeSlot[] GetAllTimeSlots()
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateTimeSlot(TimeSlot movie)
        {
            throw new System.NotImplementedException();
        }

        public TimeSlotTag[] GetTimeSlotTagsByMovieId(int movieId)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateMovie(Movie newMovie)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateTimeSlot(TimeSlot newTimeSlot)
        {
            throw new System.NotImplementedException();
        }

        public bool CreateHall(Hall newHall)
        {
            throw new System.NotImplementedException();
        }

        public bool AddRequestedSeatsToTimeSlot(SeatsProcessRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}