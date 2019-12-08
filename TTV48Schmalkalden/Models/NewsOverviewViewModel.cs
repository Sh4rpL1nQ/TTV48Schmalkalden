using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTV48Schmalkalden.Models
{
    public class NewsOverviewViewModel
    {
        public List<NewsQuickViewModel> News { get; set; } = new List<NewsQuickViewModel>();

        public NewsQuickViewModel Latest { get; set; }
    }

    public class NewsQuickViewModel
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }
    }
}
