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

        private FileModel GetDataFromFile()
        {
            var jsonFilePath = HttpContext.Server.MapPath(PathToJson);
            if (!System.IO.File.Exists(jsonFilePath))
                return null;
            var json = System.IO.File.ReadAllText(jsonFilePath);
            var fileModel = JsonConvert.DeserializeObject<FileModel>(json);
            return fileModel;
        }

    }
}