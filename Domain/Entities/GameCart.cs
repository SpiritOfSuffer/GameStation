using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameCart
    {
        private List<GameCartLine> collection = new List<GameCartLine>(); 
        public IEnumerable<GameCartLine> lines
        {
            get { return collection; }
        }
        public void AddItem(Game game, int quanity) 
        {
            GameCartLine col = collection        
                .Where(g => g.Game.Id == game.Id)
                .FirstOrDefault();               

            if (col == null)
            {
                collection.Add(new GameCartLine { Game = game, Quanity = quanity });
            }

            else
            {
                col.Quanity += quanity;
            }    
        }

        public void RemoveItem(Game game)
        {
            collection.RemoveAll(g => g.Game.Id == game.Id);
        }

        public decimal CalculateTotalPrice()
        {
            return collection.Sum(g => g.Game.Price * g.Quanity);
        }

        public void ClearCart()
        {
            if (collection != null)
            {
                collection.Clear();
            }
        }
    }

    //Создаем базовый класс, содержащий в себе выбранную игру и количество копий этой игры, которые добавили в корзину
    public class GameCartLine
    {
        public Game Game { get; set; }
        public int Quanity { get; set;  }
    }
}
