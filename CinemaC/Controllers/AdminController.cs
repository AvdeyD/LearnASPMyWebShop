using System;
using System.Linq;
using System.Web.Mvc;
using CinemaC.Attributes;
using CinemaC.Interfaces;
using CinemaC.Models;
using CinemaC.Models.Domain;
using CinemaC.Services;
using Newtonsoft.Json;

namespace CinemaC.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITicketService _ticketService;

        public AdminController()
        {
            _ticketService = new JsonTicketService(System.Web.HttpContext.Current);
        }

        public ActionResult FindMovieById(int id)
        {
            var movie = _ticketService.GetMovieById(id);
            if (movie == null)
                return Content("Movie with such ID does not exist", "applicaction/json");
            var movieJson = JsonConvert.SerializeObject(movie);
            return Content(movieJson, "applicaction/json");
        }

        public ActionResult FindHallById(int id)
        {
            var hall = _ticketService.GetHallById(id);
            if (hall == null)
                return Content("Movie with such ID does not exist", "applicaction/json");
            var hallJson = JsonConvert.SerializeObject(hall);
            return Content(hallJson, "applicaction/json");
        }

        public ActionResult FindTimeSlotById(int id)
        {
            var timeSlot = _ticketService.GetTimeSlotById(id);
            if (timeSlot == null)
                return Content("Movie with such ID does not exist", "applicaction/json");
            var timeSlotJson = JsonConvert.SerializeObject(timeSlot);
            return Content(timeSlotJson, "applicaction/json");
        }

        public ActionResult MovieList()
        {
            var movies = _ticketService.GetAllMovies();
            return View("MovieList", movies);
        }

        public ActionResult HallList()
        {
            var halls = _ticketService.GetAllHalls();
            return View("HallList", halls);
        }

        public ActionResult TimeSlotList()
        {
            return View("TimeSlotList", ProccessTimeSlots(_ticketService.GetAllTimeSlots()));
        }

        private TimeSlotGridRow[] ProccessTimeSlots(TimeSlot[] timeSlots)
        {
            var movies = _ticketService.GetAllMovies();
            var halls = _ticketService.GetAllHalls();
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
            var movie = _ticketService.GetMovieById(movieId);
            return View("EditMovie", movie);
        }

        [HttpPost]
        public ActionResult EditMovie(Movie model)
        {
            if (ModelState.IsValid)
            {
                var updateResult = _ticketService.UpdateMovie(model);
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
            var hall = _ticketService.GetHallById(hallId);
            return View("EditHall", hall);
        }

        [HttpPost]
        public ActionResult EditHall(Hall hall)
        {
            if (ModelState.IsValid)
            {
                var updateResult = _ticketService.UpdateHall(hall);
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
            var timeSlot = _ticketService.GetTimeSlotById(timeslotId);
            return View("EditTimeSlot", timeSlot);
        }

        [HttpPost]
        public ActionResult EditTimeSlot(TimeSlot timeSlot)
        {
            if (ModelState.IsValid)
            {
                var updateResult = _ticketService.UpdateTimeSlot(timeSlot);
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
                var updateResult = _ticketService.GetTimeSlotById(movieId);
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
                var result = _ticketService.CreateMovie(newMovie);
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
                var result = _ticketService.CreateTimeSlot(newTimeSlot);
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
                var result = _ticketService.CreateHall(newhall);
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