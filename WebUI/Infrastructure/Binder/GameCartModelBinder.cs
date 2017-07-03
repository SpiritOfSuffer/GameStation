using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;

namespace WebUI.Infrastructure.Binder
{
    public class GameCartModelBinder : IModelBinder
    {
        private const string sessionKey = "GameCart";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            GameCart cart = null;

            if (controllerContext.HttpContext.Session != null)
            {
                cart = (GameCart)controllerContext.HttpContext.Session[sessionKey];
            }

            if (cart == null)
            {
                cart = new GameCart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            return cart;
        }
    }
}