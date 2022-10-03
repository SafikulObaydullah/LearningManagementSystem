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
    public class SectionsController : Controller
    {
        string baseUri = "https://localhost:44372/api/Sections/";
        private List<SectionVM> GetallSections()
        {
            List<SectionVM> list = new List<SectionVM>();
            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<SectionVM>>().Result;

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
        // GET: Sections
        public ActionResult Index()
        {
            //List<SectionVM> list = new List<SectionVM>();
            //using (var client = new HttpClient())
            //{
            //    try
            //    {
            //        var result = client.GetAsync(baseUri).Result;
            //        if (result.IsSuccessStatusCode)
            //        {
            //            list = result.Content.ReadAsAsync<List<SectionVM>>().Result;
            //            return View(list);
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
            return View(GetallSections());
        }
        public List<SectionVM> GetSection()
        {
            List<SectionVM> list = new List<SectionVM>();
            list = GetallSections();
            //list = list.Where(e => e.Id == 1).ToList();
            list = list.OrderBy(l => l.SectionName).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SectionVM sectionVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PostAsJsonAsync(baseUri, sectionVM).Result;
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

            return View(sectionVM);
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
            SectionVM sectionVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        sectionVM = result.Content.ReadAsAsync<SectionVM>().Result;
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
            if (sectionVM == null)
            {
                return HttpNotFound();
            }

            return View(sectionVM);
        }
        [HttpPost]
        public ActionResult Edit(SectionVM sectionVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PutAsJsonAsync(baseUri, sectionVM).Result;
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
            return View(sectionVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SectionVM sectionVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        sectionVM = result.Content.ReadAsAsync<SectionVM>().Result;
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
            if (sectionVM == null)
            {
                return HttpNotFound();
            }

            return View(sectionVM);
        }
    }
}