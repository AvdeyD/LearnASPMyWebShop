using System;
using System.Web.Mvc;
using CinemaC.Models;
using CinemaC.Models.Domain;
using Newtonsoft.Json;

namespace CinemaC.Controllers
{
    public class AdminController : Controller
    {
        private const string Path = "/Files/Data.json";
        public ActionResult Index()
        {
            var movies = new Movie[]
            {
                new Movie
                {
                    Id = 1,
                    Description = "asdfasdf",
                    Director = "Квентин Тарантино",
                    Duration = 161,
                    Genres = new Genre[] {Genre.Comedy, Genre.Drama},
                    ImgUrl = "ImgUrlaaaa",
                    MinAge = 18,
                    Rating = 7.6f,
                    ReleaseDate = 2019,
                    Title = "Однажды в Голливуде"
                }
            };

            var halls = new Hall[]
            {
                new Hall
                {
                    Id = 1,
                    Name = "Зал 1",
                    Places = 100
                },
                new Hall
                {
                    Id = 2,
                    Name = "Зал 2",
                    Places = 100
                }
            };

            var timeSlots = new TimeSlot[]
            {
                new TimeSlot
                {
                    Id = 1,
                    Hall = halls[0],
                    Movie = movies[0],
                    Cost = 170,
                    Format = Format.TwoD,
                    StarTime = new DateTime(2019,08,26,18,00,00)
                },
                new TimeSlot
                {
                    Id = 2,
                    Hall = halls[1],
                    Movie = movies[0],
                    Cost = 350,
                    Format = Format.IMAX,
                    StarTime = new DateTime(2019,08,26,18,30,00)    
                }
            };

            var fileModel = new FileModel
            {
                Halls = halls,
                TimeSlots = timeSlots,
                Movies = movies
            };

            

            var json = JsonConvert.SerializeObject(fileModel);

            System.IO.File.WriteAllText(HttpContext.Server.MapPath(Path), json);

            return View();
        }

        public ActionResult Tickets()
        {
            var json = System.IO.File.ReadAllText(HttpContext.Server.MapPath(Path));
            var fileModel = JsonConvert.DeserializeObject<FileModel>(json);
            return View();
        }
    }
}