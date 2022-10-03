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
    public class DepartmentsController : Controller
    {
        string baseUri = "https://localhost:44372/api/Departments";
        private List<DepartmentVM> GetallDepartments()
        {
            List<DepartmentVM> list = new List<DepartmentVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<DepartmentVM>>().Result;

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
        // GET: Departments
        public ActionResult Index()
        {
            
            return View(GetallDepartments());
        }
        public List<DepartmentVM> GetDepartment()
        {
            List<DepartmentVM> list = new List<DepartmentVM>();
            list = GetallDepartments();
            list = list.OrderBy(l => l.DeptName).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Post()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Post(DepartmentVM departmentVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Departments/";
                    var result = client.PostAsJsonAsync(baseUri, departmentVM).Result;
                    if(result.IsSuccessStatusCode)
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
            return View();
        }
        public ActionResult Delete(int id)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Departments/";
                    HttpResponseMessage httpResponseMessage = client.DeleteAsync($"{baseUri}/{id}").Result;
                    if(httpResponseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Delete fail");
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            DepartmentVM departmentVM = null;
            using(var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Departments/";
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if(result.IsSuccessStatusCode)
                    {
                        departmentVM = result.Content.ReadAsAsync<DepartmentVM>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Edit fail");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
                if(departmentVM==null)
                {
                    return HttpNotFound();
                }
            }
            return View(departmentVM);
        }
        [HttpPost]
        public ActionResult Edit(DepartmentVM departmentVM)
        {
            using(var client = new HttpClient())
            {
               try
                {
                    string baseUri = "https://localhost:44372/api/Departments/";
                    var result = client.PutAsJsonAsync(baseUri, departmentVM).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "update fail");
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
            return View(departmentVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentVM departmentVM = null;
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Departments/";
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        departmentVM = result.Content.ReadAsAsync<DepartmentVM>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Edit fail");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
                if (departmentVM == null)
                {
                    return HttpNotFound();
                }
            }
            return View(departmentVM);
        }
    }
}