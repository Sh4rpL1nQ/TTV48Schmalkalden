using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTV48Schmalkalden.Models;

namespace TTV48Schmalkalden.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private DatabaseContext context;

        public HomeController(DatabaseContext context)
        {
            this.context = context;
        }

        [Route("/")]
        [Route("/startseite")]
        public IActionResult Index()
        {
            var news = context.News.Count() >= 8 ? context.News.OrderByDescending(x => x.Id).Take(8) : context.News.OrderByDescending(x => x.Id);
            var list = new NewsOverviewViewModel();
            foreach (var entry in news)
            {
                var model = new NewsQuickViewModel()
                {
                    Title = entry.Title,
                    NewsId = entry.Id,
                    ImageUrl = entry.ImageUrl
                };
                list.News.Add(model);
            }

            return View(list);
        }

        public JsonResult GetEvents()
        {
            var events = context.CalendarTasks.Include(x => x.CalendarTaskType).ToList();
            return new JsonResult(events);
        }
    }
}
