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
    public class GroupProgramsController : Controller
    {
        string baseUri = "https://localhost:44372/api/GroupPrograms/";
        private List<GroupProgramVM> GetallGroupPrograms()
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

        // GET: GroupPrograms
        public ActionResult Index()
        {
            
            return View(GetallGroupPrograms());
        }
        public List<GroupProgramVM> GetGroupProgram()
        {
            List<GroupProgramVM> list = new List<GroupProgramVM>();
            list = GetallGroupPrograms();
            list = list.OrderBy(l => l.GroupProgramName).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.InstitteTypeID = new SelectList(new InstituteTypesController().GetInstituteType(),
                "Id", "TypeName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(GroupProgramVM programVM)
        {
            using(var client = new HttpClient())
            {
                try
                {
                   
                    var result = client.PostAsJsonAsync(baseUri, programVM).Result;
                    if(result.IsSuccessStatusCode)
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
            
            return View(programVM);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    
                    HttpResponseMessage httpResponseMessage = client.DeleteAsync($"{baseUri}/{ id}").Result;
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
                GroupProgramVM groupProgram = null;
                
                using (var client = new HttpClient())
                {
                  try
                    {
                    
                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        groupProgram = result.Content.ReadAsAsync<GroupProgramVM>().Result;
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
                finally
                {

                }
                }
                if (groupProgram == null)
                {
                    return HttpNotFound();
                }

                return View(groupProgram);


        }
        [HttpPost]
        public ActionResult Edit(GroupProgramVM programVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PutAsJsonAsync(baseUri, programVM).Result;
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
            return View(programVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupProgramVM groupProgram = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        groupProgram = result.Content.ReadAsAsync<GroupProgramVM>().Result;
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
            if (groupProgram == null)
            {
                return HttpNotFound();
            }

            return View(groupProgram);


        }
    }
}