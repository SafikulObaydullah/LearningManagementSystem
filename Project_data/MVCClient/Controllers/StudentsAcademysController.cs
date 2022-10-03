using MVCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class StudentsAcademysController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}
        string baseUri = "https://localhost:44372/api/StudentsAcademys";
        //GET: StudentsAcademys
        public ActionResult Index()
        {
            List<StudentsAcademyVM> list = new List<StudentsAcademyVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<StudentsAcademyVM>>().Result;
                        return View(list);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Retrieve failed");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
            return View(list);
        }
        public ActionResult NotFound()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.StdId = new SelectList(new StudentsController().GetStudent(),
                "Id", "Name"); 
            ViewBag.ClassID = new SelectList(new ClassNamesController().GetClassName(),
                "Id", "ClassesName");
            ViewBag.SectionID = new SelectList(new SectionsController().GetSection(),
                "Id", "SectionName");
            ViewBag.ShiftID = new SelectList(new ShiftsController().GetShift(),
                "Id", "SiftName");
            ViewBag.DeptID = new SelectList(new DepartmentsController().GetDepartment(),
                "Id", "DeptName");
            ViewBag.GrpProgramID = new SelectList(new GroupProgramsController().GetGroupProgram(),
                "Id", "GroupProgramName");
            ViewBag.SemesterId = new SelectList(new SemestersController().GetSemester(),
                "Id", "SemesterName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(StudentsAcademyVM studentsAcademyVM)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.PostAsJsonAsync(baseUri, studentsAcademyVM).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Save failed");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }

            return View(studentsAcademyVM);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    HttpResponseMessage httpResponseMessage = client.DeleteAsync($"{baseUri}/{ id}").Result;
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Deleted fail");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentsAcademyVM studentsAcademyVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        studentsAcademyVM = result.Content.ReadAsAsync<StudentsAcademyVM>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Edit fail");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
            if (studentsAcademyVM == null)
            {
                return HttpNotFound();
            }

            return View(studentsAcademyVM);
        }
        [HttpPost]
        public ActionResult Edit(StudentsAcademyVM studentsAcademyVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PutAsJsonAsync(baseUri, studentsAcademyVM).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update failed");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
            return View(studentsAcademyVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentsAcademyVM studentsAcademyVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        studentsAcademyVM = result.Content.ReadAsAsync<StudentsAcademyVM>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Details fail");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
            if (studentsAcademyVM == null)
            {
                return HttpNotFound();
            }

            return View(studentsAcademyVM);
        }
    }
}