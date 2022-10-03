using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class CompetitionController : Controller
    {
        // GET: Competition
        public ActionResult Competition()
        {
            return View();  
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}