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
    public class RolesController : Controller
    {
        // GET: Roles
        public ActionResult Index()
        {
            
            return View(GetRoles());
        }
        public List<RolesVM> GetRoles()
        {
            List<RolesVM> list = new List<RolesVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Roles/";
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<RolesVM>>().Result;
                        return list;
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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RolesVM rolesVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Roles/";
                    var result = client.PostAsJsonAsync(baseUri, rolesVM).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Save fail");
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
            return View(rolesVM);
        }
        public ActionResult Delete(string id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Roles/";
                    HttpResponseMessage httpResponseMessage = client.DeleteAsync($"{baseUri}/{id}").Result;
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
                finally
                {

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
            RolesVM rolesVM = null;
            using (var client = new HttpClient())
            {
                string baseUri = "https://localhost:44372/api/Roles/";
                var result = client.GetAsync($"{baseUri}/{id}").Result;
                if (result.IsSuccessStatusCode)
                {
                    rolesVM = result.Content.ReadAsAsync<RolesVM>().Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Edit Fail");
                }
            }
            if (rolesVM == null)
            {
                return HttpNotFound();
            }
            return View(rolesVM);
        }
        [HttpPost]
        public ActionResult Edit(RolesVM roleVm)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Roles/";
                    var result = client.PutAsJsonAsync(baseUri, roleVm).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update fail");
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
            return View(roleVm);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RolesVM rolesVM = null;
            using (var client = new HttpClient())
            {
                string baseUri = "https://localhost:44372/api/Roles/";
                var result = client.GetAsync($"{baseUri}/{id}").Result;
                if (result.IsSuccessStatusCode)
                {
                    rolesVM = result.Content.ReadAsAsync<RolesVM>().Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Details Fail");
                }
            }
            if (rolesVM == null)
            {
                return HttpNotFound();
            }
            return View(rolesVM);
        }

    }
}