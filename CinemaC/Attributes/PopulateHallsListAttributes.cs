using System.Web;
using System.Web.Mvc;
using CinemaC.Interfaces;
using CinemaC.Services;
using LightInject;

namespace CinemaC.Attributes
{
    public class PopulateHallsListAttributes : ActionFilterAttribute
    {
        [Inject] private ITicketService TicketService { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["HallsList"] =
                TicketService.GetAllHalls();
            base.OnActionExecuting(filterContext);
        }
    }
}