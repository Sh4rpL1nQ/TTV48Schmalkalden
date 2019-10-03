using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTV48Schmalkalden.Models;

namespace TTV48Schmalkalden.Controllers
{    
    public class SupportController : Controller
    {
        private DatabaseContext context;

        public SupportController(DatabaseContext context)
        {
            this.context = context;
        }

        [Route("unterstuetzer-und-freunde")]
        public IActionResult Index()
        {
            var model = new SupporterViewModel();
            var supporter = context.Supporters.Include(x => x.SupportingType).OrderByDescending(x => x.Bussines).ToList();

            model.Supporter = supporter;

            return View(model);
        }
    }
}