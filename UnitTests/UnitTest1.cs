using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using System.Collections.Generic;
using Domain.Entities;
using WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.HtmlHelpers;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanPaginate()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { Id = 1, Title = "Game One"  },
                new Game { Id = 2, Title = "Game Two"  },
                new Game { Id = 3, Title = "Game Three"  },
                new Game { Id = 4, Title = "Game Four"  },
                new Game { Id = 5, Title = "Game Five"  }

            });

            GamesController contoller = new GamesController(mock.Object);
            contoller.PageCapacity = 3;

            GameListViewModel res = (GameListViewModel)contoller.List(null, 2).Model;

            List<Game> games = res.Games.ToList();
            Assert.IsTrue(games.Count == 3);
            Assert.AreEqual(games[0].Title, "Game Four");
            Assert.AreEqual(games[1].Title, "Game Five");
            Assert.AreEqual(games[2].Title, "Game Six");
        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {
            HtmlHelper helper = null;
            PaginationInfo info = new PaginationInfo
            {
                CurrentPage = 2,
                AllItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;
            MvcHtmlString result = helper.PageLinks(info, pageUrlDelegate);
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            // Организация (arrange)
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { Id = 1, Title = "Game One"  },
                new Game { Id = 2, Title = "Game Two"  },
                new Game { Id = 3, Title = "Game Three"  },
                new Game { Id = 4, Title = "Game Four"  },
                new Game { Id = 5, Title = "Game Five"  }
            });

            GamesController controller = new GamesController(mock.Object);
            controller.PageCapacity = 3;

            // Действие (act)
            GameListViewModel result = (GameListViewModel)controller.List(null, 2).Model;

            PaginationInfo pagingInfo = result.PaginationInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.AllItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }

        [TestMethod]
        public void CanFilterByCategory()
        {
            // Организация (arrange)
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { Id = 1, Title = "Game1", Genre = "Genre1"  },
                new Game { Id = 2, Title = "Game2", Genre = "Genre2"  },
                new Game { Id = 3, Title = "Game3", Genre = "Genre1"  },
                new Game { Id = 4, Title = "Game4", Genre = "Genre3"  },
                new Game { Id = 5, Title = "Game5", Genre = "Genre2"  }
            });

            GamesController controller = new GamesController(mock.Object);
            controller.PageCapacity = 3;

            // Действие (act)
            List<Game>result = ((GameListViewModel)controller.List("Genre2", 1).Model).Games.ToList();
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Title == "Game2" && result[0].Genre == "Genre2");
            Assert.IsTrue(result[1].Title == "Game5" && result[0].Genre == "Genre2");
        }

        [TestMethod]
        public void CanCreateGenres()
        {
            // Организация (arrange)
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { Id = 1, Title = "Game1", Genre = "Genre1"  },
                new Game { Id = 2, Title = "Game2", Genre = "Genre2"  },
                new Game { Id = 3, Title = "Game3", Genre = "Genre1"  },
                new Game { Id = 4, Title = "Game4", Genre = "Genre3"  },
                new Game { Id = 5, Title = "Game5", Genre = "Genre2"  }
            });

            NavController controller = new NavController(mock.Object);
            // controller.PageCapacity = 3;

            // Действие (act)
            List<string> result = ((IEnumerable<string>)controller.Menu().Model).ToList();
            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "Genre1");
            Assert.AreEqual(result[1], "Genre2");
            Assert.AreEqual(result[2], "Genre3");
        }

        [TestMethod]
        public void CanSelectGenre()
        {
            // Организация (arrange)
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { Id = 1, Title = "Game1", Genre = "Genre1"  },
                new Game { Id = 2, Title = "Game2", Genre = "Genre2"  },
                new Game { Id = 3, Title = "Game3", Genre = "Genre1"  },
                new Game { Id = 4, Title = "Game4", Genre = "Genre3"  },
                new Game { Id = 5, Title = "Game5", Genre = "Genre2"  }
            });

            NavController controller = new NavController(mock.Object);
            // controller.PageCapacity = 3;

            // Действие (act)
            string GenreToSelect = "Genre2";
            string result = controller.Menu(GenreToSelect).ViewBag.SelectedGenre;
            Assert.AreEqual(GenreToSelect, result);
        }

        [TestMethod]
        public void CanGenerateLinksByGenre()
        {
            // Организация (arrange)
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { Id = 1, Title = "Game1", Genre = "Genre1"  },
                new Game { Id = 2, Title = "Game2", Genre = "Genre2"  },
                new Game { Id = 3, Title = "Game3", Genre = "Genre1"  },
                new Game { Id = 4, Title = "Game4", Genre = "Genre3"  },
                new Game { Id = 5, Title = "Game5", Genre = "Genre2"  }
            });

            GamesController controller = new GamesController(mock.Object);
            controller.PageCapacity = 3;

            int res1 = ((GameListViewModel)controller.List("Genre1").Model).PaginationInfo.AllItems;
            int res2 = ((GameListViewModel)controller.List("Genre2").Model).PaginationInfo.AllItems;
            int res3 = ((GameListViewModel)controller.List("Genre3").Model).PaginationInfo.AllItems;
            int resAll = ((GameListViewModel)controller.List(null).Model).PaginationInfo.AllItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
