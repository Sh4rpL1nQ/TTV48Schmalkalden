using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTV48Schmalkalden.Models;

namespace TTV48Schmalkalden.Controllers
{
    public class LoginController : Controller
    {
        private DatabaseContext context;
        private Options options;
        private ISession session;

        public LoginController(DatabaseContext context, Options options, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.options = options;

            session = httpContextAccessor.HttpContext.Session;
        }

        [Route("login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User target = context.Users.SingleOrDefault(x => x.UserName == model.UserName);

                if (target == null) return RedirectToAction("Index", "Home");

                string pass = target.Password;
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: model.Password,
                    salt: Convert.FromBase64String(options.Salt),
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                if (pass.Equals(hashed))
                {
                    session.SetString("user", target.UserName);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}