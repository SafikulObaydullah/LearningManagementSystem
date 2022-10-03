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
    public class InstituteTypesController : Controller
    {
        string baseUri = "https://localhost:44372/api/InstituteTypes";
        private List<InstituteTypeVM> GetallInstituteTypes()
        {
            List<InstituteTypeVM> list = new List<InstituteTypeVM>();
            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<InstituteTypeVM>>().Result;

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
        // GET: InstituteTypes
        public ActionResult Index()
        {
            
            return View(GetallInstituteTypes());
        }
        public List<InstituteTypeVM> GetInstituteType()
        {
            List<InstituteTypeVM> list = new List<InstituteTypeVM>();
            list = GetallInstituteTypes();
            //list = list.Where(e => e.Id == 1).ToList();
            list = list.OrderBy(l => l.TypeName).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(InstituteTypeVM instituteTypeVM)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.PostAsJsonAsync(baseUri, instituteTypeVM).Result;
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

            return View(instituteTypeVM);
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
            InstituteTypeVM instituteTypeVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        instituteTypeVM = result.Content.ReadAsAsync<InstituteTypeVM>().Result;
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
            if (instituteTypeVM == null)
            {
                return HttpNotFound();
            }

            return View(instituteTypeVM);
        }
        [HttpPost]
        public ActionResult Edit(InstituteTypeVM instituteTypeVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PutAsJsonAsync(baseUri, instituteTypeVM).Result;
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
            return View(instituteTypeVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstituteTypeVM instituteTypeVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        instituteTypeVM = result.Content.ReadAsAsync<InstituteTypeVM>().Result;
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
            if (instituteTypeVM == null)
            {
                return HttpNotFound();
            }

            return View(instituteTypeVM);
        }

    }
}