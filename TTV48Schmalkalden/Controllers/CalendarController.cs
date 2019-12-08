using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTV48Schmalkalden.Controllers
{
    public class CalendarController : Controller
    {
        private DatabaseContext context;
        private ISession session;

        public CalendarController(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;

            session = httpContextAccessor.HttpContext.Session;
        }

        [Route("kalender")]
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            var events = context.CalendarTasks.Include(x => x.CalendarTaskType).ToList();
            return new JsonResult(events);
        }
    }
}
