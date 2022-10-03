using MVCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace MVCClient.Controllers
{
    public class HomeWorksController : Controller
    {
        string baseUri = "https://localhost:44372/api/HomeWorks/";
        public ActionResult Homework(int? i)
        {
            List<HomeworkVM> list = new List<HomeworkVM>();
            if (HttpContext.Session["token"] != null)
            {
                if (HttpContext.Session["std"] != null)
                {
                    StudentVM studentVM = (StudentVM)HttpContext.Session["std"];
                    using (var client = new HttpClient())
                    {
                        try
                        {
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme: "Bearer",
                                parameter: HttpContext.Session["token"].ToString());
                            var l = new SubjectsController().GetallSubjects().ToList();
                            ViewBag.SubName = new SelectList(l, "Id", "SubName");
                            var result = client.GetAsync(baseUri + "?classId=6 & secId=1 & shiftId=1").Result;
                            if (result.IsSuccessStatusCode)
                            {

                                list = result.Content.ReadAsAsync<List<HomeworkVM>>().Result;
                                return View(list.ToPagedList(i ?? 1,3));
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
                }
                else
                {
                    return RedirectToAction("Login", "Account", new
                    {
                        returnURL = "HomeWorks/Homework"

                    });
                }
            }
            else
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnURL = "HomeWorks/Homework"

                });
            }
            return View(list);
        }
        private List<HomeworkVM> GetallHomeWorks()
        {
            List<HomeworkVM> list = new List<HomeworkVM>();
            //using (var client = new HttpClient())
            //{
            //    try
            //    {
            //        var result = client.GetAsync(baseUri).Result;
            //        if (result.IsSuccessStatusCode)
            //        {
            //            list = result.Content.ReadAsAsync<List<HomeworkVM>>().Result;

            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "Retrieve failed");
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ModelState.AddModelError("", ex.Message);
            //    }
            //    finally
            //    {

            //    }
            //}
            return list;

        }
        //GET: HomeWorks
        public ActionResult Index()
        {
            
            List<HomeworkVM> list = new List<HomeworkVM>();
            if (HttpContext.Session["token"] != null)
            {
                if (HttpContext.Session["std"] != null)
                {
                    StudentVM studentVM = (StudentVM)HttpContext.Session["std"];
                    using (var client = new HttpClient())
                    {
                        try
                        {
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme: "Bearer", 
                                parameter: HttpContext.Session["token"].ToString());

                            var result = client.GetAsync(baseUri + "?classId=4&secId=1&shiftId=1").Result;
                            if (result.IsSuccessStatusCode)
                            {
                                list = result.Content.ReadAsAsync<List<HomeworkVM>>().Result;
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
                }
            }

            else
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnURL = "UserManagers/Index"

                });
            }
            return View(list);

        }
        public List<HomeworkVM> GetHomeWork()
        {
            List<HomeworkVM> list = new List<HomeworkVM>();
            list = GetallHomeWorks();
            list = list.OrderBy(e => e.ClassId == 1).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.TeacherID = new SelectList(new EmployeesController().GetTeacher(),
                "Id", "Name");
            ViewBag.SubId = new SelectList(new SubjectsController().GetSubject(),
                "Id", "SubName");
            ViewBag.ClassId = new SelectList(new ClassNamesController().GetClassName(),
                "Id", "ClassesName");
            ViewBag.SecID = new SelectList(new SectionsController().GetSection(),
                "Id", "SectionName");
            ViewBag.ShiftId = new SelectList(new ShiftsController().GetShift(),
                "Id", "SiftName");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(HomeworkVM homeworkVM)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.PostAsJsonAsync(baseUri, homeworkVM).Result;
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

            return View(homeworkVM);
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
            HomeworkVM homeworkVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        homeworkVM = result.Content.ReadAsAsync<HomeworkVM>().Result;
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
            if (homeworkVM == null)
            {
                return HttpNotFound();
            }

            return View(homeworkVM);
        }
        [HttpPost]
        public ActionResult Edit(HomeworkVM homeworkVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PutAsJsonAsync(baseUri, homeworkVM).Result;
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
            return View(homeworkVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeworkVM homeworkVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        homeworkVM = result.Content.ReadAsAsync<HomeworkVM>().Result;
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
            if (homeworkVM == null)
            {
                return HttpNotFound();
            }

            return View(homeworkVM);
        }
    }
}