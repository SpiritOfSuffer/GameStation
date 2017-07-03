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
    public class GamesController : Controller
    {
        private IGameRepository rep;
        public int PageCapacity = 3;
        public GamesController(IGameRepository repository)
        {
            rep = repository;
        }

        public ViewResult List(string genre, int page = 1)
        {
            GameListViewModel view = new GameListViewModel
            {
                Games = rep.Games
                .Where(g => genre == null || g.Genre == genre)
                .OrderBy(game => game.Id)
                .Skip((page - 1) * PageCapacity)
                .Take(PageCapacity),
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageCapacity,
                    AllItems = genre == null ? 
                        rep.Games.Count() : 
                        rep.Games.Where(game => game.Genre == genre).Count()
                },
                CurrentGenre = genre       
            };

            return View(view);
        }

        public FileContentResult GetImage(int gameId)
        {
            Game game = rep.Games
                .FirstOrDefault(g => g.Id == gameId);

            if (game != null)
            {
                return File(game.ImageData, game.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}