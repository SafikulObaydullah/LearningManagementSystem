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
    public class HomeWorkFilesController : Controller
    {

        // GET: HomeWorkFiles
        public ActionResult Index()
        {
            List<HomeWorkFileVM> list = new List<HomeWorkFileVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/HomeWorkFiles";
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<HomeWorkFileVM>>().Result;
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
        [HttpGet]
        public ActionResult Create()
        {
            List<SubmittedHomeworkVM> list = new List<SubmittedHomeworkVM>();
            using(var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/SubmittedHomeworks";
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<SubmittedHomeworkVM>>().Result;
                    }
                }
                catch(Exception ex)
                {
                    
                }
            }
            ViewBag.SubmittedHomeworkId = new SelectList(list.OrderBy(l => l.SubmittedDate).ToList(), "Id", "SubmittedDate");
            return View();
        }
        [HttpPost]
        public ActionResult Create(HomeWorkFileVM homeWorkFileVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/HomeWorkFiles";
                    var result = client.PostAsJsonAsync(baseUri, homeWorkFileVM).Result;
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

            return View(homeWorkFileVM);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    HttpResponseMessage httpResponseMessage = client.DeleteAsync("https://localhost:44372/api/HomeWorkFiles/" + id).Result;
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
            HomeWorkFileVM homeWorkFileVM = null;

            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/HomeWorkFiles";


                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        homeWorkFileVM = result.Content.ReadAsAsync<HomeWorkFileVM>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {

                }
            }
            if (homeWorkFileVM == null)
            {
                return HttpNotFound();
            }

            return View(homeWorkFileVM);
        }
        public ActionResult Edit(HomeWorkFileVM homeWorkFileVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/HomeWorkFiles";
                    var result = client.PutAsJsonAsync(baseUri, homeWorkFileVM).Result;
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
            return View(homeWorkFileVM);
        }
        public ActionResult Details(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HomeWorkFileVM homeWorkFileVM = null;

            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/HomeWorkFiles";


                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        homeWorkFileVM = result.Content.ReadAsAsync<HomeWorkFileVM>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {

                }
            }
            if (homeWorkFileVM == null)
            {
                return HttpNotFound();
            }

            return View(homeWorkFileVM);
        }
    }
}