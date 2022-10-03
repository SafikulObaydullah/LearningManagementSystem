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
    public class ClassNamesController : Controller
    {
        string baseUri = "https://localhost:44372/api/ClassNames";
        private List<ClassNameVM> GetallClassNames()
        {
            List<ClassNameVM> list = new List<ClassNameVM>();
            try
            {
                if (System.Web.HttpContext.Current.Session["token"] != null)
                {

                    using (var client = new HttpClient())
                    {
                        try
                        {
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme: "Bearer",
                                parameter: System.Web.HttpContext.Current.Session["token"].ToString());
                            var result = client.GetAsync(baseUri).Result;
                            if (result.IsSuccessStatusCode)
                            {
                                list = result.Content.ReadAsAsync<List<ClassNameVM>>().Result;
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
                }
                else
                {
                    return list;
                }
            } 

            catch (Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            finally 
            { 

            }
            
            return list;

        }
        //GET: ClassNames
        public ActionResult Index()
        {
            List<ClassNameVM> list = new List<ClassNameVM>();
           
            if (HttpContext.Session["token"] != null)
            {
                return View(GetallClassNames());
            }
            else
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnURL = "UserManagers/Index"
                });
            }
            return View(list);
        }
        public List<ClassNameVM> GetClassName()
        {
            List<ClassNameVM> list = new List<ClassNameVM>();
            list = GetallClassNames();
            //list = list.Where(e => e.Id == 1).ToList();
            list = list.OrderBy(l => l.ClassesName).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Post()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Post(ClassNameVM classNameVM)
        {
            using (var client = new HttpClient())
            {
               try
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme: "Bearer",
                        parameter: System.Web.HttpContext.Current.Session["token"].ToString());
                    var result = client.PostAsJsonAsync(baseUri, classNameVM).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Save fail");
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
            return View(classNameVM);
        }
        public ActionResult Delete(int id)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    
                    HttpResponseMessage httpResponseMessage = client.DeleteAsync($"{baseUri}/{id}").Result;
                    if(httpResponseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Deleted fail");
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
            ClassNameVM classNameVM = null;
            using(var client = new HttpClient())
            {
                
                var result = client.GetAsync($"{baseUri}/{id}").Result;
                if(result.IsSuccessStatusCode)
                {
                    classNameVM = result.Content.ReadAsAsync<ClassNameVM>().Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Edit Fail");
                }
            }
            if(classNameVM==null)
            {
                return HttpNotFound();
            }
            return View(classNameVM);
        }
        [HttpPost]
        public ActionResult Edit(ClassNameVM classNameVM)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    
                    var result = client.PutAsJsonAsync(baseUri, classNameVM).Result;
                    if (result.IsSuccessStatusCode)
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
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
            }
            return View(classNameVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassNameVM classNameVM = null;
            using (var client = new HttpClient())
            {

                var result = client.GetAsync($"{baseUri}/{id}").Result;
                if (result.IsSuccessStatusCode)
                {
                    classNameVM = result.Content.ReadAsAsync<ClassNameVM>().Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Details Fail");
                }
            }
            if (classNameVM == null)
            {
                return HttpNotFound();
            }
            return View(classNameVM);
        }
    }
}