using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Models
{
    public class GameListViewModel
    {
        public IEnumerable<Game> Games { get; set;  }
        public PaginationInfo PaginationInfo { get; set; }
        public string CurrentGenre { get; set; }
    }
}