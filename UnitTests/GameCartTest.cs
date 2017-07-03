using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class GameCartTest
    {
        [TestMethod]
        public void CanAddItem()
        {
            //Создаем организацию из 2 игр
            Game game1 = new Game{ Id = 1, Title = "TestGame1"};
            Game game2 = new Game { Id = 2, Title = "TestGame2" };

            //Создаем объект класса GameCart и вызываем метод AddItem, в котором передаем две наших игры
            GameCart gamecart = new GameCart();

            gamecart.AddItem(game1, 1);
            gamecart.AddItem(game2, 1);
     
            //Кладем в список типа GameCartLine поля с переданными играми
            List<GameCartLine> collection = gamecart.lines.ToList();

            //Делаем проверку на кол-во элементов списка, и на содержание каждого из элементов списка
            Assert.AreEqual(collection.Count(), 2);
            Assert.AreEqual(collection[0].Game, game1);
            Assert.AreEqual(collection[1].Game, game2);

        }

        [TestMethod]
        public void CanAddQuanityForExistingItem()
        {
            //Создаем организацию из 2 игр
            Game game1 = new Game { Id = 1, Title = "TestGame1" };
            Game game2 = new Game { Id = 2, Title = "TestGame2" };

            //Создаем объект класса GameCart и вызываем метод AddItem, в котором передаем две наших игры
            GameCart gamecart = new GameCart();

            gamecart.AddItem(game1, 1);
            gamecart.AddItem(game2, 1);
            gamecart.AddItem(game1, 5); 

            //Кладем в список типа GameCartLine поля с переданными играми
            List<GameCartLine> collection = gamecart.lines.OrderBy(c => c.Game.Id).ToList();

            //Делаем проверку на количество добавленных товаров в корзину
            Assert.AreEqual(collection.Count(), 2);
            Assert.AreEqual(collection[0].Quanity, 6);
            Assert.AreEqual(collection[1].Quanity, 1);

        }

        [TestMethod]
        public void CanRemoveItems()
        {
            //Создаем организацию из 2 игр
            Game game1 = new Game { Id = 1, Title = "TestGame1" };
            Game game2 = new Game { Id = 2, Title = "TestGame2" };

            //Создаем объект класса GameCart и вызываем методы AddItem() и RemoveItemFromCart()
            GameCart gamecart = new GameCart();

            gamecart.AddItem(game1, 1);
            gamecart.AddItem(game2, 2);         //Добавили один экземпляр gamе1 и 2 экземпляра game2
                                                //На данном этапе у нас 2 поля с товарами в корзине
            gamecart.RemoveItem(game1); //Но после вызова метода RemoveItemFromCart(), в корзине останется только одно поле с game2 в количестве двух экземпляров


            //Делаем проверку на количество элементов в корзине
            Assert.AreEqual(gamecart.lines.Count(), 1);
        }

        [TestMethod]
        public void CanCalculateTotalPrice()
        {
            //Создаем организацию из 2 игр
            Game game1 = new Game { Id = 1, Title = "TestGame1", Price = 220 };
            Game game2 = new Game { Id = 2, Title = "TestGame2", Price = 150 };

            //Создаем объект класса GameCart и вызываем методы AddItem() и CalculateTotalPrice()
            GameCart gamecart = new GameCart();

            gamecart.AddItem(game1, 1);
            gamecart.AddItem(game2, 2);

            decimal result = gamecart.CalculateTotalPrice();

            //Делаем проверку на итоговую цену в корзине
            Assert.AreEqual(result, 520);
        }

        [TestMethod]
        public void CanClearCart()
        {
            //Создаем организацию из 2 игр
            Game game1 = new Game { Id = 1, Title = "TestGame1", Price = 220 };
            Game game2 = new Game { Id = 2, Title = "TestGame2", Price = 150 };

            //Создаем объект класса GameCart и вызываем методы AddItem() и ClearCart()
            GameCart gamecart = new GameCart();

            gamecart.AddItem(game1, 1);
            gamecart.AddItem(game2, 2); 
                                        //Добавили 2 поля с играми
            gamecart.ClearCart();       //А теперь вызываем метод ClearCart(), и наша корзина очищается

            //Делаем проверку на количество полей в корзине
            Assert.AreEqual(gamecart.lines.Count(), 0);
        }

      /*  [TestMethod]
        public void CanAddItemToCart()
        {
            
        } */
    }
}
