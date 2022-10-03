using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class CalendarsController : Controller
    {
        // GET: Calendars
        public ActionResult Calendars()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}