using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaC.Interfaces;
using CinemaC.Models.Tickets;
using CinemaC.Services;
using Newtonsoft.Json;

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
            var timeSlot = _ticketService.GetTimeSlotById(timeslotId);
            var model = new HallInfo()
            {
                ColumnsCount = 20,
                RowsCount = 12,
                TicketCost = timeSlot.Cost,
                CurrentTimeSlotId = timeslotId,
                RequestedSeats = timeSlot.RequestedSeats
            };
            return View("HallInfo", model);

        }

        public string ProcessRequest(SeatsProcessRequest request)
        {
            var result = _ticketService.AddRequestedSeatsToTimeSlot(request);
            return JsonConvert.SerializeObject(new {requestResult = result });
        }
    }
}