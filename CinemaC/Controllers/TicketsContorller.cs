using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaC.Interfaces;
using CinemaC.Services;

namespace CinemaC.Controllers
{
    public class TicketsController : Controller
    {
        private ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public ActionResult GetMovies()
        {
            var allMovies = _ticketService.GetFullMoviesInfo();
            return View("~/Views/Tickets/MoviesList.cshtml", allMovies);
        }

        public ActionResult GetHallInfo(int timeslotId)
        {
            return View("HallInfo");
        }
    }
}