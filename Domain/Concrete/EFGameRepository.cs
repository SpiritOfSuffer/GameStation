using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    //Cоздаем хранилище данных, которое наследует интерфейс IGameRepository, и использует объект класса EFDbContext для извлечения данных из БД, юзая Entity Framework 
    public class EFGameRepository : IGameRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Game> Games
        {
            get { return context.Games; }
        }

        public void SaveGame(Game game)
        {
            if (game.Id == 0)
            {
                context.Games.Add(game);
            }
            else
            {
                Game dbEntry = context.Games.Find(game.Id);

                if (dbEntry != null)
                {
                    dbEntry.Title = game.Title;
                    dbEntry.Description = game.Description;
                    dbEntry.Developer = game.Developer;
                    dbEntry.Genre = game.Genre;
                    dbEntry.Price = game.Price;
                    dbEntry.ImageData = game.ImageData;
                    dbEntry.ImageMimeType = game.ImageMimeType;
                }
            }

            context.SaveChanges();
        }

        public Game DeleteGame(int gameId)
        {
            Game dbEntry = context.Games.Find(gameId);

            if (dbEntry != null)
            {
                context.Games.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;
        }
    }
}
