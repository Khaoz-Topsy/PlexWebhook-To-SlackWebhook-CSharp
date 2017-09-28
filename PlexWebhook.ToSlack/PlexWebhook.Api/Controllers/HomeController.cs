using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlexWebhook.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return new FilePathResult("~/HomePage/index.html", "text/html");
        }
    }
}
