using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class SubmittedHomeworksAjaxController : Controller
    {
        // GET: SubmittedHomeworksAjax
        public ActionResult Index()
        {
            return View();
        }
    }
}