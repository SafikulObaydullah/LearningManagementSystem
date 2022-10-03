using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class DesignationsAjaxController : Controller
    {
        // GET: DesignationsAjax
        public ActionResult Index()
        {
            ViewBag.DeptId = new SelectList(new DepartmentsController().GetDepartment(),
                "Id", "DeptName");
            return View();
        }
    }
}