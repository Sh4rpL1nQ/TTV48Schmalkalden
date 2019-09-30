using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using TTV48Schmalkalden.Models;

namespace TTV48Schmalkalden.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext context;

        public HomeController(DatabaseContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var news = context.News.Count() >= 8 ? context.News.OrderByDescending(x => x.Id).Take(8) : context.News.OrderByDescending(x => x.Id);
            var list = new NewsOverviewViewModel();
            foreach (var entry in news)
            {
                var model = new NewsQuickViewModel()
                {
                    Title = entry.Title,
                    NewsId = entry.Id
                };
                list.News.Add(model);
            }
            return View(list);
        }

        public IActionResult Redirection(string tag)
        {
            return Redirect(Url.RouteUrl(new { controller = "Home", action = "Index" }) + "#" + tag);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
