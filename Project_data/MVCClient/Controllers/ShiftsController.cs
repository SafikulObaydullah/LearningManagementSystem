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
    public class ShiftsController : Controller
    {
        string baseUri = "https://localhost:44372/api/Shifts/";
        private List<ShiftVM> GetallShifts()
        {
            List<ShiftVM> list = new List<ShiftVM>();
            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<ShiftVM>>().Result;

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
        // GET: Shifts
        public ActionResult Index()
        {
            //List<ShiftVM> list = new List<ShiftVM>();
            //using (var client = new HttpClient())
            //{
            //    try
            //    {
            //        //string baseUri = "https://localhost:44372/api/Shifts/";
            //        var result = client.GetAsync(baseUri).Result;
            //        if(result.IsSuccessStatusCode)
            //        {

            //            list = result.Content.ReadAsAsync<List<ShiftVM>>().Result;
            //            return View(list);
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "Retrieve fail");
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
            return View(GetallShifts());
        }
        public List<ShiftVM> GetShift()
        {
            List<ShiftVM> list = new List<ShiftVM>();
            list = GetallShifts();
            //list = list.Where(e => e.Id == 1).ToList();
            list = list.OrderBy(l => l.SiftName).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Post()
        {
            
            return View();
        }
        public ActionResult Post(ShiftVM shiftVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PostAsJsonAsync(baseUri, shiftVM).Result;
                    if(result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Save fail");
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShiftVM shiftVM = null;
            using (var client = new HttpClient())
            {
                try
                { 
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if(result.IsSuccessStatusCode)
                    {
                        shiftVM = result.Content.ReadAsAsync<ShiftVM>().Result;
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
                if(shiftVM==null)
                {
                    return HttpNotFound();
                }
            }
            return View(shiftVM);
        }
        [HttpPost]
        public ActionResult Edit(ShiftVM shiftVM)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    var result = client.PutAsJsonAsync(baseUri,shiftVM).Result;
                    if(result.IsSuccessStatusCode)
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
            return View(shiftVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShiftVM shiftVM = null;
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        shiftVM = result.Content.ReadAsAsync<ShiftVM>().Result;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Details fail");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                finally
                {

                }
                if (shiftVM == null)
                {
                    return HttpNotFound();
                }
            }
            return View(shiftVM);
        }
    }
}