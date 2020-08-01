using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaC.Binders;

namespace CinemaC.Models
{
    [ModelBinder(typeof(LoginBinder))]
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        

    }
}