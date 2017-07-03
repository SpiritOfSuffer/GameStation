using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class GameCartIndexViewModel
    {
        public GameCart Cart { get; set; }
        public string ReturnUrl { get; set; }

    }
}