using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ShippingInfo
    {
        [Required(ErrorMessage = "Укажите ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите адрес доставки")]
        [Display(Name = "Первый адрес доставки")]
        public string Address1 { get; set; }

        [Display(Name = "Второй адрес доставки")]
        public string Address2 { get; set; }

        [Display(Name = "Третий адрес доставки")]
        public string Address3 { get; set; }

        [Required(ErrorMessage = "Укажите город")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(ErrorMessage = "Страна")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
