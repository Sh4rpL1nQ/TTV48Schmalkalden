using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTV48Schmalkalden.Models;

namespace TTV48Schmalkalden.Controllers
{
    public class NewsController : Controller
    {
        private const int MaxDisplayedNews = 5;
        private const int MaxBodyDigits = 200;
        private const string ToBeContinued = " ...";

        private DatabaseContext context;

        public NewsController(DatabaseContext context)
        {
            this.context = context;
        }

        public IActionResult Index(int id, int category)
        {
            var list = new NewsListViewModel();
            var news = context.News.OrderByDescending(x => x.Id);
            var comments = context.Comments.Include(x => x.News);
            var hasCategories = context.HasCategories.Include(x => x.News).Include(x => x.Category);
            var newsWithCategory = hasCategories.Where(y => y.CategoryId == category)
                .Select(d => d.News)
                .OrderByDescending(x => x.Id); ;
            var categories = context.Categories;

            foreach (var cat in categories)
            {
                list.Categories.Add(new CategoryViewModel()
                {
                    Category = cat,
                    Count = hasCategories.Where(x => x.CategoryId == cat.Id).Count()
                });
            }

            if (id == 0) id = 1;
            int skip = (id - 1) * MaxDisplayedNews;

            var raw = (category == 0) ? news : newsWithCategory;

            if (raw.Count() > skip)
            {
                var test = raw.Count();
                var data = (raw.Count() - skip >= MaxDisplayedNews) ? 
                    raw.Skip(skip).Take(MaxDisplayedNews) : 
                    raw.Skip(skip);

                list.Pages = raw.Count() % MaxDisplayedNews > 0 ?
                    (raw.Count() / MaxDisplayedNews) + 1 :
                    raw.Count() / MaxDisplayedNews;

                foreach (var entry in data)
                {
                    var model = new NewsViewModel()
                    {
                        NewsId = entry.Id,
                        Title = entry.Title,
                        Written = entry.Written,
                        Author = entry.Author,
                        Body = entry.Body.Length >= MaxBodyDigits ?
                            (entry.Body.Substring(0, MaxBodyDigits) + ToBeContinued) :
                            entry.Body.ToString(),
                        Categories = hasCategories.Where(x => x.NewsId == entry.Id).Select(y => y.Category).ToList(),
                        CommentCount = comments.Where(y => y.News.Id == entry.Id).Count()
                    };

                    list.News.Add(model);
                }
            }
            return View(list);
        }

        public IActionResult Detail(int id = 1)
        {
            var targetNews = context.News.FirstOrDefault(x => x.Id == id);
            var comments = context.Comments.Include(x => x.News).Where(y => y.News.Id == id).OrderByDescending(x => x.Written).ToList();
            var newsCategories = context.HasCategories.Where(x => x.NewsId == id).Select(y => y.Category).ToList();
            var allCategories = context.Categories;

            var model = new NewsPostViewModel();

            if (id == 0) { return View(model); }

            foreach (var comment in comments)
            {
                model.Comments.Add(new CommentViewModel()
                {
                    Id = comment.Id,
                    Author = comment.Author,
                    Written = comment.Written,
                    Body = comment.Body,
                    News = comment.News
                });
            }

            model.NewsViewModel = new NewsViewModel()
            {
                NewsId = targetNews.Id,
                Title = targetNews.Title,
                Written = targetNews.Written,
                Author = targetNews.Author,
                Body = targetNews.Body,
                Categories = newsCategories,
                CommentCount = comments.Count,
            };

            foreach (var cat in allCategories)
            {
                model.Categories.Add(new CategoryViewModel()
                {
                    Category = cat,
                    Count = context.HasCategories.Where(x => x.CategoryId == cat.Id).Count()
                });
            }

            model.CommentForm.NewsId = model.NewsViewModel.NewsId;

            return View(model);
        }

        [HttpPost]
        public IActionResult AddComment(NewsPostViewModel model)
        {
            var targetNews = context.News.SingleOrDefault(x => x.Id == model.CommentForm.NewsId);
            var comment = new Comment()
            {
                Author = model.CommentForm.Name,
                Written = Convert.ToDateTime(DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss")),
                Body = model.CommentForm.Message,
                News = targetNews
            };

            context.Comments.Add(comment);

            context.SaveChanges();

            return RedirectToAction("Detail", "News", new { id = targetNews.Id });
        }
    }
}