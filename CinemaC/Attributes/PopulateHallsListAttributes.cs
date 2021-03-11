using System.Web;
using System.Web.Mvc;
using CinemaC.Interfaces;
using CinemaC.Services;
using LightInject;

namespace CinemaC.Attributes
{
    public class PopulateHallsListAttributes : ActionFilterAttribute
    {
        [Inject] public ITicketService TicketService { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var x = TicketService.GetAllHalls();
            filterContext.Controller.ViewData["HallList"] = x;
                
            base.OnActionExecuting(filterContext);
        }
    }
}