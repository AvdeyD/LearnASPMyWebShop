using System;
using System.Web.Mvc;
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
    }
}