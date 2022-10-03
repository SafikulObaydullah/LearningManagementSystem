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
    public class SubjectsController : Controller
    {
        string baseUri = "https://localhost:44372/api/Subjects";
        public List<SubJectVM> GetallSubjects()
        {
            List<SubJectVM> list = new List<SubJectVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<SubJectVM>>().Result;
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
        // GET: Subjects
        public ActionResult Index()
        {
            List<SubJectVM> list = new List<SubJectVM>();
            //using(var client = new HttpClient())
            //{
            //    try
            //    {
            //        string baseUri = "https://localhost:44372/api/Subjects";
            //        var result = client.GetAsync(baseUri).Result;
            //        if(result.IsSuccessStatusCode)
            //        {
            //            list = result.Content.ReadAsAsync<List<SubJectVM>>().Result;
            //            return View(list);
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "Retrieve failed");
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        ModelState.AddModelError("", ex.Message);
            //    }
            //    finally
            //    {

            //    }
            //}
            return View(GetallSubjects());
        }
        public List<SubJectVM> GetSubject()
        {
            List<SubJectVM> list = new List<SubJectVM>();
            list = GetallSubjects();
            //list = list.Where(e => e.Id==1).ToList();
            list = list.OrderBy(l => l.SubName).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Post()
        {
            List<GroupProgramVM> list = new List<GroupProgramVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<GroupProgramVM>>().Result;

                    }

                }
                catch (Exception ex)
                {
                    
                }
            }
            ViewBag.GroupProgramId = new SelectList(list.OrderBy(l => l.GroupProgramName).ToList(), "Id", "GroupProgramName");
            return View();
        }
        [HttpPost]
        public ActionResult Post(SubJectVM subJectVM)
        {
            
            using(var client = new HttpClient())
            {
                try
                {
                    var result = client.PostAsJsonAsync(baseUri,subJectVM).Result;
                    if(result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Save failed");
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
            return View(subJectVM);
        }
        public ActionResult Delete(int  id)
        {
            using(var client = new HttpClient())
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
                        return RedirectToAction("Delete fail");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
        
            }
            return View();
        }
        public ActionResult Edit(string id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            SubJectVM subJectVM = null;
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if(result.IsSuccessStatusCode)
                    {
                        subJectVM = result.Content.ReadAsAsync<SubJectVM>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Edit fail");
                    }
                }
                
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                if (subJectVM == null)
                {
                    return HttpNotFound();
                }
            }
            return View(subJectVM);
        }
        [HttpPost]
        public ActionResult Edit(SubJectVM subJectVM)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    var result = client.PutAsJsonAsync(baseUri, subJectVM).Result;
                    if(result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("","Update fail");
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
            return View(subJectVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            SubJectVM subJectVM = null;
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        subJectVM = result.Content.ReadAsAsync<SubJectVM>().Result;
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
                if (subJectVM == null)
                {
                    return HttpNotFound();
                }
            }
            return View(subJectVM);
        }

    }
}