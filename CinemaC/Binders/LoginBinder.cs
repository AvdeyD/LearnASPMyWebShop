using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using CinemaC.Models;
using IModelBinder = System.Web.Mvc.IModelBinder;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;

namespace CinemaC.Binders
{
    public class LoginBinder: IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = new LoginModel
            {
                Login = controllerContext.HttpContext.Request.Form["Login"],
                Password = controllerContext.HttpContext.Request.Form["Password"]
            };
            return model;
        }
    }
}