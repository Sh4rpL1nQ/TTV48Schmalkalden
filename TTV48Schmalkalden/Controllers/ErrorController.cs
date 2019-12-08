using Microsoft.AspNetCore.Mvc;

namespace TTV48Schmalkalden.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {

        public ErrorController()
        {
            
        }

        [Route("internal-server-error")]
        public IActionResult InternalServerError()
        {
            return View();
        }
    }
}