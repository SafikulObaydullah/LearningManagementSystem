using MVCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class SemestersController : Controller
    {
        string baseUri = "https://localhost:44372/api/Semesters/";
        private List<SemesterVM> GetallSemesters()
        {
            List<SemesterVM> list = new List<SemesterVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<SemesterVM>>().Result;

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
            return list;

        }
        // GET: Semesters
        public ActionResult Index()
        {
            //List<SemesterVM> list = new List<SemesterVM>();
            //using (var client = new HttpClient())
            //{
            //    try
            //    {

            //        var result = client.GetAsync(baseUri).Result;
            //        if(result.IsSuccessStatusCode)
            //        {

            //            list = result.Content.ReadAsAsync<List<SemesterVM>>().Result;
            //            return View(list);
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "Retrieve fail");
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        ModelState.AddModelError("", ex.Message);
            //    }
            //    finally
            //    {

            //    }
            //}
            return View(GetallSemesters());
        }
        public List<SemesterVM> GetSemester()
        {
            List<SemesterVM> list = new List<SemesterVM>();
            list = GetallSemesters();
            list = list.OrderBy(l => l.SemesterName).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Post()
        {

            return View();
        }
        public ActionResult Post(SemesterVM semesterVM)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    var result = client.PostAsJsonAsync(baseUri, semesterVM).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Save fail");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
            return View(semesterVM);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Semesters/" + id;
                    HttpResponseMessage httpResponseMessage = client.DeleteAsync($"{baseUri}/{id}").Result;
                    if(httpResponseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Delete fail");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            SemesterVM semesterVM = null;
            using(var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Semesters/";
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if(result.IsSuccessStatusCode)
                    {
                        semesterVM = result.Content.ReadAsAsync<SemesterVM>().Result;
                        
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
           return View(semesterVM);
        }
        [HttpPost]
        public ActionResult Edit(SemesterVM semesterVM)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Semesters/";
                    var result = client.PutAsJsonAsync(baseUri, semesterVM).Result;
                    if(result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update fail");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Edit fail");
                }
                finally
                {
                    
                }
            }
            return View();
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            SemesterVM semesterVM = null;
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Semesters/";
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        semesterVM = result.Content.ReadAsAsync<SemesterVM>().Result;

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
            return View(semesterVM);
        }
    }
}