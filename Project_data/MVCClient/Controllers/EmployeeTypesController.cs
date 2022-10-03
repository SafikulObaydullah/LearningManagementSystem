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
    public class EmployeeTypesController : Controller
    {

        string baseUri = "https://localhost:44372/api/EmployeeTypes";
        private List<EmployeeTypeVM> GetallEmployeeTypes()
        {
            List<EmployeeTypeVM> list = new List<EmployeeTypeVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<EmployeeTypeVM>>().Result;

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

            return View(GetallEmployeeTypes());
        }
        public List<EmployeeTypeVM> GetEmployeeType()
        {
            List<EmployeeTypeVM> list = new List<EmployeeTypeVM>();
            list = GetallEmployeeTypes();
            list = list.OrderBy(e => e.TypeName).ToList();
            return list;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeTypeVM employeeTypeVM )
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/EmployeeTypes";
                    var result = client.PostAsJsonAsync(baseUri, employeeTypeVM).Result;
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

            return View(employeeTypeVM);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    HttpResponseMessage httpResponseMessage = client.DeleteAsync("https://localhost:44372/api/EmployeeTypes/" + id).Result;
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
            EmployeeTypeVM employeeTypeVM = null;

            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/EmployeeTypes";


                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        employeeTypeVM = result.Content.ReadAsAsync<EmployeeTypeVM>().Result;
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
            if (employeeTypeVM == null)
            {
                return HttpNotFound();
            }

            return View(employeeTypeVM);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeTypeVM employeeTypeVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/EmployeeTypes";
                    var result = client.PutAsJsonAsync(baseUri, employeeTypeVM).Result;
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
            return View(employeeTypeVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeTypeVM employeeTypeVM = null;

            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/EmployeeTypes";


                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        employeeTypeVM = result.Content.ReadAsAsync<EmployeeTypeVM>().Result;
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
            if (employeeTypeVM == null)
            {
                return HttpNotFound();
            }

            return View(employeeTypeVM);
        }

    }
}