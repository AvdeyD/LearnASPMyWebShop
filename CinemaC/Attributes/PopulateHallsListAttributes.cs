using System.Web;
using System.Web.Mvc;
using CinemaC.Services;

namespace CinemaC.Attributes
{
    public class PopulateHallsListAttributes : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ticketServise = new JsonTicketService(HttpContext.Current);
            filterContext.Controller.ViewData["HallsList"] =
                ticketServise.GetAllHalls();
            base.OnActionExecuting(filterContext);
        }
    }
}