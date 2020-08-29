using System;
using System.Linq;
using System.Web.Mvc;
using CinemaC.Attributes;
using CinemaC.Interfaces;
using CinemaC.Models;
using CinemaC.Models.Domain;
using CinemaC.Services;
using LightInject;
using Newtonsoft.Json;

namespace CinemaC.Controllers
{
    public class AdminController : Controller
    {
        [Inject]
        public ITicketService TicketService { get; set; }
    

        
        public ActionResult FindMovieById(int id)
        {
            var movie = TicketService.GetMovieById(id);
            if (movie == null)
                return Content("Movie with such ID does not exist", "applicaction/json");
            var movieJson = JsonConvert.SerializeObject(movie);
            return Content(movieJson, "applicaction/json");
        }

        public ActionResult FindHallById(int id)
        {
            var hall = TicketService.GetHallById(id);
            if (hall == null)
                return Content("Movie with such ID does not exist", "applicaction/json");
            var hallJson = JsonConvert.SerializeObject(hall);
            return Content(hallJson, "applicaction/json");
        }

        public ActionResult FindTimeSlotById(int id)
        {
            var timeSlot = TicketService.GetTimeSlotById(id);
            if (timeSlot == null)
                return Content("Movie with such ID does not exist", "applicaction/json");
            var timeSlotJson = JsonConvert.SerializeObject(timeSlot);
            return Content(timeSlotJson, "applicaction/json");
        }

        public ActionResult MovieList()
        {
            var movies = TicketService.GetAllMovies();
            return View("MovieList", movies);
        }

        public ActionResult HallList()
        {
            var halls = TicketService.GetAllHalls();
            return View("HallList", halls);
        }

        public ActionResult TimeSlotList()
        {
            return View("TimeSlotList", ProccessTimeSlots(TicketService.GetAllTimeSlots()));
        }

        private TimeSlotGridRow[] ProccessTimeSlots(TimeSlot[] timeSlots)
        {
            var movies = TicketService.GetAllMovies();
            var halls = TicketService.GetAllHalls();
            return timeSlots.Select(timeSlot => new TimeSlotGridRow()
            {
                StarTime = timeSlot.StarTime,
                Cost = timeSlot.Cost,
                Format = timeSlot.Format,
                Id = timeSlot.Id,
                Hall = halls.First(x => x.Id == timeSlot.HallId),
                Movie = movies.First(x => x.Id == timeSlot.MovieId)
            }).ToArray();
        }


        [HttpGet]
        public ActionResult EditMovie(int movieId)
        {
            var movie = TicketService.GetMovieById(movieId);
            return View("EditMovie", movie);
        }

        [HttpPost]
        public ActionResult EditMovie(Movie model)
        {
            if (ModelState.IsValid)
            {
                var updateResult = TicketService.UpdateMovie(model);
                if (updateResult)
                {
                    return RedirectToAction("MovieList");
                }

                return Content("Update failed.");
            }

            return View("EditMovie", model);
        }

        [HttpGet]
        public ActionResult EditHall(int hallId)
        {
            var hall = TicketService.GetHallById(hallId);
            return View("EditHall", hall);
        }

        [HttpPost]
        public ActionResult EditHall(Hall hall)
        {
            if (ModelState.IsValid)
            {
                var updateResult = TicketService.UpdateHall(hall);
                if (updateResult)
                {
                    return RedirectToAction("HallList");
                }

                return Content("Update failed.");
            }

            return View("EditHall", hall);
        }

        [HttpGet]
        [PopulateHallsListAttributes, PopulateMoviesListAttributes]
        public ActionResult EditTimeSlot(int timeslotId)
        {
            var timeSlot = TicketService.GetTimeSlotById(timeslotId);
            return View("EditTimeSlot", timeSlot);
        }

        [HttpPost]
        public ActionResult EditTimeSlot(TimeSlot timeSlot)
        {
            if (ModelState.IsValid)
            {
                var updateResult = TicketService.UpdateTimeSlot(timeSlot);
                if (updateResult)
                {
                    return RedirectToAction("TimeSlotList");
                }

                return Content("Update failed.");
            }

            return View("EditTimeSlot", timeSlot);
        }

        [HttpGet]
        public ActionResult GetMovieTimesSlotsList(int movieId)
        {
            if (ModelState.IsValid)
            {
                var updateResult = TicketService.GetTimeSlotById(movieId);
                return View("EditTimeSlot", updateResult);
            }

            return RedirectToAction("MovieList");
        }
        [HttpGet]
        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie(Movie newMovie)
        {
            if (ModelState.IsValid)
            {
                var result = TicketService.CreateMovie(newMovie);
                if (result)
                {
                    return RedirectToAction("MovieList");
                }
                return Content("Update failed");
            }
            return View(newMovie);
        }

        [HttpGet]
        [PopulateHallsListAttributes, PopulateMoviesListAttributes]
        public ActionResult AddTimeSlot()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTimeSlot(TimeSlot newTimeSlot)
        {
            if (ModelState.IsValid)
            {
                var result = TicketService.CreateTimeSlot(newTimeSlot);
                if (result)
                {
                    return RedirectToAction("TimeSlotList");
                }
                return Content("Update failed");
            }
            return View(newTimeSlot);
        }

        [HttpGet]
        [PopulateHallsListAttributes, PopulateMoviesListAttributes]
        public ActionResult AddHall()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddHall(Hall newhall)
        {
            if (ModelState.IsValid)
            {
                var result = TicketService.CreateHall(newhall);
                if (result)
                {
                    return RedirectToAction("HallList");
                }
                return Content("Update failed");
            }
            return View(newhall);
        }
    }
}