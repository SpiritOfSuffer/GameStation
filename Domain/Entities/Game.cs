using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Game
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите название игры")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Введите описание к игре")]
        public string Description { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Введите описание к игре")]
        public string Developer { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Введите категорию")]
        public string Genre { get; set; }

        [Display(Name = "Цена (руб)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
