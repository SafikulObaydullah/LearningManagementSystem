using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class StudentsAjaxController : Controller
    {
        // GET: StudentsAjax
        public ActionResult Index()
        {
            return View();
        }
    }
}