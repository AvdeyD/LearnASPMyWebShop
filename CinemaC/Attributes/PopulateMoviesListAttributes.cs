using System.Web;
using System.Web.Mvc;
using CinemaC.Services;

namespace CinemaC.Attributes
{
    public class PopulateMoviesListAttributes: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ticketServise = new JsonTicketService(HttpContext.Current);
            filterContext.Controller.ViewData["MoviesList"] =
                ticketServise.GetAllMovies();
            base.OnActionExecuting(filterContext);
        }
    }
}