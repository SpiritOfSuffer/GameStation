using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Models;
 

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IGameRepository rep;
        private IOrderProcessor processor;
        public CartController(IGameRepository repository, IOrderProcessor orderProcessor)
        {
            rep = repository;
            processor = orderProcessor;
        }

        public ViewResult Index(GameCart cart, string returnUrl)
        {
            return View(new GameCartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public ViewResult Checkout()
        {
            return View(new ShippingInfo());
        }

        [HttpPost]
        public ViewResult Checkout(GameCart cart, ShippingInfo shippingInfo)
        {
            if (cart.lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста");
            }

            if (ModelState.IsValid)
            {
                processor.ProcessOrder(cart, shippingInfo);
                cart.ClearCart();

                return View("Completed");
            }
            else
            {
                return View(shippingInfo);
            }
        }

        public PartialViewResult GameCartSummary(GameCart cart)
        {
            return PartialView(cart);
        }

      /*  public GameCart GetCart()
        {
            GameCart cart = (GameCart)Session["GameCart"]; // В объекте Session изменен ключ с "Cart" на "GameCart"

            if (cart == null)
            {
                cart = new GameCart();
                Session["GameCart"] = cart;
            }
 
            return cart;
        } */

        public RedirectToRouteResult AddItemToCart(GameCart cart, int gameId, string returnUrl) 
        {                                                          
            Game game = rep.Games                                                
                .FirstOrDefault(g => g.Id == gameId);                             
                                                                                  
            if (game != null)                                                      
            {
                cart.AddItem(game, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveItemFromCart(GameCart cart, int gameId, string returnUrl)
        {
            Game game = rep.Games
                .FirstOrDefault(g => g.Id == gameId);

            if (game != null)
            {
                cart.RemoveItem(game);
            }

            return RedirectToAction("Index", new { returnUrl});
        }
    }
}
