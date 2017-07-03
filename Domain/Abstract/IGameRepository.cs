using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IGameRepository
    {
        //Используем интерфейс IEnumerable, чтобы позволить вызывающему коду получать последовательность объектов Game, не сообщая как и где хранятся данные
        IEnumerable<Game> Games { get; }
        void SaveGame(Game game);
        Game DeleteGame(int gameId);
        

        
    }
}
