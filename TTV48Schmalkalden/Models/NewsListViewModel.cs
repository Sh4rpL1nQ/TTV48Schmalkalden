using Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TTV48Schmalkalden.Models
{
    public class NewsListViewModel
    {
        public List<NewsViewModel> News { get; set; } = new List<NewsViewModel>();

        public int Pages { get; set; }

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        public string Password { get; set; }
    }

    public class NewsViewModels
    {
        public List<AdminNewsViewModel> News { get; set; } = new List<AdminNewsViewModel>();
    }

    public class CategoryViewModel
    {
        public Category Category { get; set; }

        public int Count { get; set; }
    }

    public class NewsPostViewModel
    {
        public NewsViewModel NewsViewModel { get; set; }

        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        public CommentForm CommentForm { get; set; } = new CommentForm();

        public string Password { get; set; }
    }

    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Body { get; set; }

        public DateTime Written { get; set; }

        public News News { get; set; }

        public string WrittenDefference()
        {
            var now = DateTime.Now;
            var result = now.Subtract(Written);
            if (result.Days >= 360)
                return "1 Jahr";
            else if (result.Days >= 720)
            {
                int res = result.Days / 360;
                return res + " Jahre";
            }
            else if (result.Days / 30 > 1)
            {
                int integer = result.Days / 30;
                return integer + " Monate";
            }
            else if (result.Days / 30 == 1)
                return "1 Monat";
            else if (result.Days > 1)
                return result.Days + " Tage";
            else if (result.Days == 1)
                return "1 Tag";
            else if (result.Hours > 1)
                return result.Hours + " Stunden";
            else if (result.Hours == 1)
                return "1 Stunde";
            else if (result.Minutes > 1)
                return result.Minutes + " Minuten";
            else if (result.Minutes == 1)
                return "1 Minute";
            else
                return result.Seconds + " Sekunden";                
        }
    }

    public class CommentForm
    {
        public string Name { get; set; }

        public string Message { get; set; }

        public int NewsId { get; set; }
    }

    public class AdminNewsViewModel
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }

    public class NewsViewModel
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Body { get; set; }

        public DateTime Written { get; set; }

        public int CommentCount { get; set; }

        public List<Category> Categories { get; set; }

        public string GetMonth()
        {
            if (Written != null)
            {
                CultureInfo info = new CultureInfo("de-DE");
                return Written.ToString("MMMM", info);
            }

            return string.Empty;
        }

        public string GetDay()
        {
            if (Written != null)
            {
                return Written.ToString("dd");
            }

            return string.Empty;
        }

        public string GetYear()
        {
            if (Written != null)
            {
                return Written.ToString("yyyy");
            }

            return string.Empty;
        }
    }
}
