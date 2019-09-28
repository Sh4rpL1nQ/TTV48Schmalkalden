using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;

namespace TTV48Schmalkalden.Controllers
{
    public class SitemapController : Controller
    {
        private DatabaseContext context;

        public SitemapController(DatabaseContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            List<SitemapNode> nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index", "Home"))
                {
                    ChangeFrequency = ChangeFrequency.Weekly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.9M
                },
                new SitemapNode(Url.Action("Index", "Contact"))
                {
                    ChangeFrequency = ChangeFrequency.Never,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.9M
                },
                new SitemapNode(Url.Action("Index", "PrivacyPolicy"))
                {
                    ChangeFrequency = ChangeFrequency.Never,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.9M
                },
                new SitemapNode(Url.Action("Index", "News"))
                {
                    ChangeFrequency = ChangeFrequency.Weekly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.9M
                }
            };

            var news = context.News.ToList();
            foreach (var entry in news)
            {
                nodes.Add(new SitemapNode(Url.Action("Detail", "News", new { id = entry.Id }))
                {
                    ChangeFrequency = ChangeFrequency.Weekly,
                    LastModificationDate = DateTime.UtcNow,
                    Priority = 0.6M
                });
            }

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}