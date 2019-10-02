using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment hostingEnvironment;

        public AdminController(DatabaseContext context, IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;

            session = httpContextAccessor.HttpContext.Session;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            if (session.GetString("user") == null) return RedirectToAction("PageNotFound", "Error");

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
            if (session.GetString("user") == null) return RedirectToAction("PageNotFound", "Error");
            if (id < 1) return RedirectToAction("PageNotFound", "Error");

            if (ModelState.IsValid)
            {
                var images = context.Images.AsNoTracking().Include(x => x.News).Where(x => x.News.Id == id).ToList();
                if (images != null && images.Count > 0)
                    context.Images.RemoveRange(images);

                context.SaveChanges();

                News news = new News() { Id = id };
                context.News.Remove(news);
                context.SaveChanges();
            }
            
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Edit(int id)
        {
            if (session.GetString("user") == null) return RedirectToAction("PageNotFound", "Error");
            if (id < 1) return RedirectToAction("PageNotFound", "Error");

            var model = new EditNewsViewModel();

            if (ModelState.IsValid)
            {
                var targetNews = context.News.SingleOrDefault(x => x.Id == id);
                var imageData = context.Images.Include(x => x.News);

                //Images
                List<Image> images = new List<Image>();
 
                model.News = new News()
                {
                    Id = targetNews.Id,
                    Written = targetNews.Written,
                    Author = targetNews.Author,
                    Body = targetNews.Body,
                    Title = targetNews.Title
                };
                images = imageData.Where(x => x.News.Id == targetNews.Id).ToList();

                var categories = context.Categories.Where(x => !x.Name.Equals("Alle")).ToList();
                var newsCategories = context.HasCategories.Include(x => x.News).Include(x => x.Category)?
                    .Where(y => y.News.Id == id && !y.Category.Name.Equals("Alle"))?
                    .Select(y => y.Category);

                categories.ToList().ForEach(x => model.AllCategories.Add(x));

                model.Categories = newsCategories.Select(x => x.Id.ToString()).ToArray();
                model.Images = images;
                model.UploadImagesViewModel = new UploadImagesViewModel();
            }

            return View(model);
        }

        public IActionResult Create()
        {
            if (session.GetString("user") == null) return RedirectToAction("PageNotFound", "Error");

            //Images
            var model = new EditNewsViewModel();
            model.Images = context.Images.Include(x => x.News).Where(x => x.News == null).ToList();

            model.News = new News()
            {
                Written = DateTime.Now,
                Author = context.Users.SingleOrDefault(x => x.UserName.Equals(session.GetString("user")))?.FullName,
                Body = "",
                Title = ""
            };

            var categories = context.Categories.Where(x => !x.Name.Equals("Alle")).ToList();
            var newsCategories = context.HasCategories.Include(x => x.News).Include(x => x.Category)?
                .Where(y => y.News.Id == 0 && !y.Category.Name.Equals("Alle"))?
                .Select(y => y.Category);

            model.AllCategories = categories.ToList();

            model.Categories = new string[] { };
            model.UploadImagesViewModel = new UploadImagesViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(EditNewsViewModel model)
        {
            if (session.GetString("user") == null) return RedirectToAction("PageNotFound", "Error");

            var news = model.News;
            var categories = model.Categories;

            //Update New
            context.News.Add(model.News);
            context.SaveChanges();

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

            var newImages = context.Images.Where(x => x.News == null);
            foreach (var img in newImages)
            {
                img.News = context.News.Last();
                context.Images.Update(img);
            }

            context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public IActionResult Edit(EditNewsViewModel model)
        {
            if (session.GetString("user") == null) return RedirectToAction("PageNotFound", "Error");

            var news = model.News;
            var categories = model.Categories;
            var oldCategories = context.HasCategories.Include(x => x.News).Include(x => x.Category)
                .Where(y => y.News.Id == news.Id && !y.Category.Name.Equals("Alle"))
                .Select(y => y.Category).ToList();

            //Update New
            context.News.Update(model.News);
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

        [HttpPost]
        public IActionResult ImageUpload(EditNewsViewModel model)
        {
            if (session.GetString("user") == null) return RedirectToAction("PageNotFound", "Error");

            var image = model.UploadImagesViewModel.File;
            var targetNews = context.News.SingleOrDefault(x => x.Id == model.News.Id);

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (image != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, @"assets\images\news");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    image.CopyTo(new FileStream(filePath, FileMode.Create));

                    context.Images.Add(new Image()
                    {
                        ImageDescription = model.UploadImagesViewModel.Description,
                        ImageUrl = "/assets/images/news/" + uniqueFileName,
                        News = targetNews
                    });

                    context.SaveChanges();
                }
            }
            if (targetNews == null) return RedirectToAction("Create", "Admin");
            else return RedirectToAction("Edit", "Admin", new { id = targetNews.Id });
        }
    }
}