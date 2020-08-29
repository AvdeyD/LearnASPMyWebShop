using System.Web;
using System.Web.Mvc;
using CinemaC.Interfaces;
using CinemaC.Services;
using LightInject;

namespace CinemaC.Attributes
{
    public class PopulateMoviesListAttributes: ActionFilterAttribute
    {
        [Inject] private ITicketService TicketService { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
          
            filterContext.Controller.ViewData["MoviesList"] =
                TicketService.GetAllMovies();
            base.OnActionExecuting(filterContext);
        }
    }
}