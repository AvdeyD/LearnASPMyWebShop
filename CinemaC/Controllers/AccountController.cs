using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaC.Models;

namespace CinemaC.Controllers
{

    public class AccountController : Controller
    {
        [HttpGet]// GET: Account}
        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                return View("LoginResult");
            }
            return View();
        }
    }
}