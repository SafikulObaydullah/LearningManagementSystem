using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class HolidaysController : Controller
    {
        // GET: Holidays
        public ActionResult Holidays()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}