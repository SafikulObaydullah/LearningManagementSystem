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
    public class EmployeesController : Controller
    {
        string baseUri = "https://localhost:44372/api/Employees";
        private List<EmployeeVM> GetallEmployees()
        {
            List<EmployeeVM> list = new List<EmployeeVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<EmployeeVM>>().Result;
                        
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
        // GET: Employees
        public ActionResult Index()
        {
            return View(GetallEmployees());
        }
        public List<EmployeeVM> GetTeacher()
        {
            List<EmployeeVM> list = new List<EmployeeVM>();
            list = GetallEmployees();
            list = list.OrderBy(e => e.Name).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.DesId = new SelectList(new DesignationsController().GetDesignation(), "Id", "DesgName");

            
            ViewBag.DeptId = new SelectList(new DepartmentsController().GetDepartment(), "Id", "DeptName");
            
            ViewBag.TypeID = new SelectList(new EmployeeTypesController().GetEmployeeType(),
                "Id", "TypeName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeVM employeeVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Employees";
                    var result = client.PostAsJsonAsync(baseUri, employeeVM).Result;
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

            return View(employeeVM);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {

                    HttpResponseMessage httpResponseMessage = client.DeleteAsync("https://localhost:44372/api/Employees/" + id).Result;
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
            EmployeeVM employeeVM = null;

            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Employees";


                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        employeeVM = result.Content.ReadAsAsync<EmployeeVM>().Result;
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
            if (employeeVM == null)
            {
                return HttpNotFound();
            }

            return View(employeeVM);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeVM employeeVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Employees";
                    var result = client.PutAsJsonAsync(baseUri, employeeVM).Result;
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
            return View(employeeVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeVM employeeVM = null;

            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Employees";


                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        employeeVM = result.Content.ReadAsAsync<EmployeeVM>().Result;
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
            if (employeeVM == null)
            {
                return HttpNotFound();
            }

            return View(employeeVM);
        }
    }
}