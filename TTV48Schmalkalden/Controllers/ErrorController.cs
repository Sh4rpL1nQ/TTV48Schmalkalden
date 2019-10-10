using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TTV48Schmalkalden.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        private readonly TelemetryClient _telemetryClient;

        public ErrorController(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        [Route("internal-server-error")]
        public IActionResult InternalServerError()
        {
            return View();
        }

        [Route("page-not-found")]
        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}