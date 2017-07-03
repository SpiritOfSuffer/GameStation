using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Abstract;

namespace WebUI.Controllers
{
    public class NavController : Controller
    {
        private IGameRepository rep;

        public NavController(IGameRepository repository)
        {
            rep = repository;
        }
        public PartialViewResult Menu(string genre = null)
        {
            ViewBag.SelectedGenre = genre;
            IEnumerable<string> genres = rep.Games
                .Select(game => game.Genre)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(genres);
        }
    }
    
}