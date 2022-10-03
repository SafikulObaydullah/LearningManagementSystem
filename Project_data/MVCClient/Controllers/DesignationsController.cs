using MVCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class DesignationsController : Controller
    {
        string baseUri = "https://localhost:44372/api/Designations";
        private List<DesignationVM> GetallDesignations()
        {
            List<DesignationVM> list = new List<DesignationVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<DesignationVM>>().Result;

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
        // GET: HomeWorks
        public ActionResult Index()
        {

            return View(GetallDesignations());
        }
        public List<DesignationVM> GetDesignation()
        {
            List<DesignationVM> list = new List<DesignationVM>();
            list = GetallDesignations();
            list = list.OrderBy(e => e.DesgName).ToList();
            return list;
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<DepartmentVM> list = new List<DepartmentVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Departments";
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<DepartmentVM>>().Result;

                    }

                }
                catch (Exception ex)
                {

                }
            }
            ViewBag.DeptId = new SelectList(list.OrderBy(l => l.DeptName).ToList(), "Id", "DeptName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(DesignationVM designationVM)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    //string baseUri = "https://localhost:44372/api/Designations";
                    var result = client.PostAsJsonAsync(baseUri, designationVM).Result;
                    if(result.IsSuccessStatusCode)
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
            return View(designationVM);
        }
        public ActionResult Delete(int id)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    //string baseUri = "https://localhost:44372/api/Designations";
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
                finally
                {

                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            if(id==null)
            {
                HttpStatusCodeResult httpStatusCodeResult = new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DesignationVM designationVM = null;
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if(result.IsSuccessStatusCode)
                    {
                        designationVM = result.Content.ReadAsAsync<DesignationVM>().Result;
                       
                    }
                    else
                    {
                        return RedirectToAction("", "Edit fail");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
                if(designationVM==null)
                {
                    return HttpNotFound();
                }
            }
            return View(designationVM);
        }
        [HttpPost]
        public ActionResult Edit(DesignationVM designationVM)
        {
            using(var client = new HttpClient())
            {
                var result = client.PutAsJsonAsync(baseUri, designationVM).Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("","Update fail");
                }
            }
            return View(designationVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                HttpStatusCodeResult httpStatusCodeResult = new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            DesignationVM designationVM = null;
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        designationVM = result.Content.ReadAsAsync<DesignationVM>().Result;

                    }
                    else
                    {
                        return RedirectToAction("", "Edit fail");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
                if (designationVM == null)
                {
                    return HttpNotFound();
                }
            }
            return View(designationVM);
        }
    }
}