using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Abstract;
using Domain.Concrete;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IGameRepository repository;

        public AdminController (IGameRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Games);
        }

        public ViewResult Edit(int gameId)
        {
            Game game = repository.Games
                .FirstOrDefault(g => g.Id == gameId);

            return View(game);
        }

        public ViewResult Create()
        {
            return View("Edit", new Game());
        }

        [HttpPost]
        public ActionResult Edit(Game game, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    game.ImageMimeType = image.ContentType;
                    game.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(game.ImageData, 0, image.ContentLength);
                }

                repository.SaveGame(game);
                TempData["message"] = string.Format("Изменения в игре \"{0}\" были сохранены", game.Title);
                return RedirectToAction("Index");
            }
            else
            {
                return View(game);
            }
            
        }

        [HttpPost]
        public ActionResult Delete(int gameId)
        {
            Game deletedGame = repository.DeleteGame(gameId);

            if (deletedGame != null)
            {
                TempData["message"] = string.Format("Игра \"{0}\" была удалена",
                    deletedGame.Title);
            }
            return RedirectToAction("Index");
        }
    }
}