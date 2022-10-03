using MVCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class SubjectsAjaxController : Controller
    {
        // GET: SubjectsAjax
        public List<GroupProgramVM> GetProgram()
        {
            List<GroupProgramVM> list   = new List<GroupProgramVM>();
            return list;
        }
        public ActionResult Index()
        {
            ViewBag.GroupProgramId = new SelectList(new GroupProgramsController().GetGroupProgram(),
                "Id", "GroupProgramName");
            
            return View();
        }
        
    }
}