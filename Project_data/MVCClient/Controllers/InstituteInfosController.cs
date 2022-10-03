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
    public class InstituteInfosController : Controller
    {
        string baseUri = "https://localhost:44372/api/InstituteInfos";
        private List<InstituteInfoVM> GetallInstituteInfos()
        {
            List<InstituteInfoVM> list = new List<InstituteInfoVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<InstituteInfoVM>>().Result;

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
        // GET: InstituteInfos
        public ActionResult Index()
        {
            //List<InstituteInfoVM> list = new List<InstituteInfoVM>();
            //using (var client = new HttpClient())
            //{
            //    try
            //    {
            //        var result = client.GetAsync(baseUri).Result;
            //        if (result.IsSuccessStatusCode)
            //        {
            //            list = result.Content.ReadAsAsync<List<InstituteInfoVM>>().Result;
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
            return View(GetallInstituteInfos());
        }
        public List<InstituteInfoVM> GetInstituteInfo()
        {
            List<InstituteInfoVM> list = new List<InstituteInfoVM>();
            list = GetallInstituteInfos();
            list = list.OrderBy(l => l.InstituteName).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.TypeID = new SelectList(new EmployeeTypesController().GetEmployeeType(),
                "Id", "TypeName");

            return View();
        }
        [HttpPost]
        public ActionResult Create(InstituteInfoVM instituteInfoVM)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.PostAsJsonAsync(baseUri, instituteInfoVM).Result;
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

            return View(instituteInfoVM);
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
            InstituteInfoVM instituteInfoVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        instituteInfoVM = result.Content.ReadAsAsync<InstituteInfoVM>().Result;
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
            if (instituteInfoVM == null)
            {
                return HttpNotFound();
            }

            return View(instituteInfoVM);
        }
        [HttpPost]
        public ActionResult Edit(InstituteInfoVM instituteInfoVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PutAsJsonAsync(baseUri, instituteInfoVM).Result;
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
            return View(instituteInfoVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstituteInfoVM instituteInfoVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        instituteInfoVM = result.Content.ReadAsAsync<InstituteInfoVM>().Result;
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
            if (instituteInfoVM == null)
            {
                return HttpNotFound();
            }

            return View(instituteInfoVM);
        }
    }
}