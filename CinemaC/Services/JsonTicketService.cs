using System;
using System.IO;
using System.Linq;
using System.Web;
using CinemaC.Interfaces;
using CinemaC.Models;
using CinemaC.Models.Domain;
using Newtonsoft.Json;

namespace CinemaC.Services
{
    public class JsonTicketService: ITicketService
    {
        private const string PathToJson = "/Files/Data.json";
        private HttpContext HttpContext { get; set; }

        public JsonTicketService(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }

        public Movie GetMovieById(int id)
        {
            var fullModel = GetDataFromFile();
            return fullModel.Movies.FirstOrDefault(x => x.Id == id);
        }

        public Movie[] GetAllMovies()
        {
            var fullModel = GetDataFromFile();
            return fullModel.Movies;
        }

        public Hall GetHallById(int id)
        {
            var fullModel = GetDataFromFile();
            return fullModel.Halls.FirstOrDefault(x => x.Id == id);
        }

        public Hall[] GetAllHalls()
        {
            var fullModel = GetDataFromFile();
            return fullModel.Halls;
        }



        public TimeSlot GetTimeSlotById(int id)
        {
            var fullModel = GetDataFromFile();
            return fullModel.TimeSlots.FirstOrDefault(x => x.Id == id);
        }

        public TimeSlot[] GetAllTimeSlots()
        {
            var fullModel = GetDataFromFile();
            return fullModel.TimeSlots;
        }

        public bool UpdateMovie(Movie movie)
        {
            var fullModel = GetDataFromFile();
            var movieToUpdate = fullModel.Movies.FirstOrDefault(x => x.Id == movie.Id);
            if (movieToUpdate == null)
            {
                return false;
            }

            movieToUpdate.Title = movie.Title;
            movieToUpdate.MinAge = movie.MinAge;
            movieToUpdate.Director = movie.Director;
            movieToUpdate.Duration = movie.Duration;
            movieToUpdate.Description = movie.Description;
            movieToUpdate.ImgUrl = movie.ImgUrl;
            if(movie.Genres != null)movieToUpdate.Genres = movie.Genres;
            movieToUpdate.Rating = movie.Rating;
            movieToUpdate.ReleaseDate = movie.ReleaseDate;
            SaveToFile(fullModel);
            return true;
        }

        public bool UpdateTimeSlot(TimeSlot timeSlot)
        {
            var fullModel = GetDataFromFile();
            var timeSlotsToUpdate = fullModel.TimeSlots.FirstOrDefault(x => x.Id == timeSlot.Id);
            if (timeSlotsToUpdate == null)
            {
                return false;
            }

            timeSlotsToUpdate.StarTime = timeSlot.StarTime;
            timeSlotsToUpdate.Format = timeSlot.Format;
            timeSlotsToUpdate.Cost = timeSlot.Cost;
            timeSlotsToUpdate.MovieId = timeSlot.MovieId;
            timeSlotsToUpdate.HallId = timeSlot.HallId;
            SaveToFile(fullModel);
            return true;
        }

        public bool CreateMovie(Movie newMovie)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newMovieId = fullModel.Movies.Max(m => m.Id) + 1;
                newMovie.Id = newMovieId;
                var existingMoviesList = fullModel.Movies.ToList();
                existingMoviesList.Add(newMovie);
                fullModel.Movies = existingMoviesList.ToArray();
                SaveToFile(fullModel);
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }

        public TimeSlot[] GeTimeSlotsByMoveId(int moveId)
        {
            var fullModel = GetDataFromFile();
            return fullModel.TimeSlots.Where(x => x.MovieId == moveId).ToArray(); 
        }

        public bool UpdateHall(Hall hall)
        {
            var fullModel = GetDataFromFile();
            var hallToUpdate = fullModel.Halls.FirstOrDefault(x => x.Id == hall.Id);
            if (hallToUpdate == null)
            {
                return false;
            }

            hallToUpdate.Name = hall.Name;
            hallToUpdate.Places = hall.Places;
            SaveToFile(fullModel);
            return true;
        }


        private void SaveToFile(FileModel model)
        {
            var jsonFilePath = HttpContext.Server.MapPath(PathToJson);
            var serializeModel = JsonConvert.SerializeObject(model);
            File.WriteAllText(jsonFilePath, serializeModel);
        }


        private FileModel GetDataFromFile()
        {
            var jsonFilePath = HttpContext.Server.MapPath(PathToJson);
            if (!System.IO.File.Exists(jsonFilePath))
                return null;
            var json = System.IO.File.ReadAllText(jsonFilePath);
            var fileModel = JsonConvert.DeserializeObject<FileModel>(json);
            return fileModel;
        }


        public bool CreateTimeSlot(TimeSlot newTimeSlot)
        {
            var fullModel = GetDataFromFile();
            try
            {
                var newTimeSlotId = fullModel.TimeSlots.Max(m => m.Id) + 1;
                newTimeSlot.Id = newTimeSlotId;
                var existingTimeSlotsList = fullModel.TimeSlots.ToList();
                existingTimeSlotsList.Add(newTimeSlot);
                fullModel.TimeSlots = existingTimeSlotsList.ToArray();
                SaveToFile(fullModel);
            }
            catch (Exception exception)
            {
                return false;
            }

            return true;
        }
    }
}