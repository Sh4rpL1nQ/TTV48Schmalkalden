using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TTV48Schmalkalden.Models;

namespace TTV48Schmalkalden.Controllers
{
    public class AdminController : Controller
    {
        private DatabaseContext context;
        private ISession session;

        public AdminController(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;

            session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Index()
        {
            if (session.GetString("user") == null) return RedirectToAction("Home", "Error");

            var news = context.News.OrderByDescending(x => x.Written);
            var model = new NewsViewModels();

            foreach (var entry in news)
            {
                model.News.Add(new AdminNewsViewModel()
                {
                    NewsId = entry.Id,
                    Title = entry.Title
                });
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            //if (session.GetString("user") == null) return RedirectToAction("Home", "Error");

            News news = new News() { Id = id };
            context.News.Attach(news);
            context.News.Remove(news);
            context.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Edit(int id)
        {
            if (session.GetString("user") == null) return RedirectToAction("Home", "Error");

            var targetNews = context.News.SingleOrDefault(x => x.Id == id);
            var model = new EditNewsViewModel();
            if (targetNews == null && id == 0)
            {
                model.News = new News()
                {
                    Id = id,
                    Written = DateTime.Now,
                    Author = context.Users.SingleOrDefault(x => x.UserName.Equals(session.GetString("user")))?.FullName,
                    Body = string.Empty,
                    Title = string.Empty
                };
            }
            else
            {
                model.News = new News()
                {
                    Written = targetNews.Written,
                    Author = targetNews.Author,
                    Body = targetNews.Body,
                    Title = targetNews.Title
                };
            }

            var categories = context.Categories.Where(x => !x.Name.Equals("Alle")).ToList();
            var newsCategories = context.HasCategories.Include(x => x.News).Include(x => x.Category)?
                .Where(y => y.News.Id == id && !y.Category.Name.Equals("Alle"))?
                .Select(y => y.Category);

            categories.ToList().ForEach(x => model.AllCategories.Add(x));

            model.Categories = newsCategories.Select(x => x.Id.ToString()).ToArray();

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditNewsViewModel model)
        {
            if (session.GetString("user") == null) return RedirectToAction("Home", "Error");
            var news = model.News;
            var categories = model.Categories;
            var oldCategories = context.HasCategories.Include(x => x.News).Include(x => x.Category)
                .Where(y => y.News.Id == news.Id && !y.Category.Name.Equals("Alle"))
                .Select(y => y.Category).ToList();

            //Update New
            context.Update(model.News);
            context.SaveChanges();

            //Remove all categories from news
            foreach (var cat in oldCategories)
            {
                context.HasCategories.Remove(new HasCategory() {
                    CategoryId = cat.Id,
                    NewsId = model.News.Id
                });
            }

            //Add all new categories for news
            var newCategories = new List<Category>();
            foreach (var cat in categories)
                newCategories.Add(context.Categories.FirstOrDefault(x => x.Id.Equals(int.Parse(cat))));
            
            foreach (var cat in newCategories)
            {
                context.HasCategories.Add(new HasCategory()
                {
                    CategoryId = cat.Id,
                    NewsId = model.News.Id
                });
            }

            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
    }
}