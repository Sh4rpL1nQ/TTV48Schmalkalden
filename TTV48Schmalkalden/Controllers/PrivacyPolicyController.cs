using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TTV48Schmalkalden.Controllers
{
    public class PrivacyPolicyController : Controller
    {
        [Route("datenschutzerklaerung")]
        public IActionResult Index()
        {
            return View();
        }
    }
}