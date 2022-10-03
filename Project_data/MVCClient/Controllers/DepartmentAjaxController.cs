using Project_data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class DepartmentAjaxController : Controller
    {
        DbLMSContext db = new DbLMSContext();
        // GET: DepartmentAjax
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetAll()
        {
            return Json(db.Departments.ToList(),JsonRequestBehavior.AllowGet);  
        }
        public JsonResult GetById(int id)
        {
            return Json(db.Departments.Find(id),JsonRequestBehavior.AllowGet);
        }
        public JsonResult Save(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
            return Json(department, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(Department department)
        {
            db.Entry(department).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(department,JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            var dept = db.Departments.Find(id);
            db.Departments.Remove(dept);
            db.SaveChanges();
            return Json(dept, JsonRequestBehavior.AllowGet);
        }
    }
}