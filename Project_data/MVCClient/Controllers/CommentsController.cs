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
    public class CommentsController : Controller
    {
        string baseUri = "https://localhost:44372/api/Comments";
        private List<CommentVM> GetallComments()
        {
            List<CommentVM> list = new List<CommentVM>();
            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<CommentVM>>().Result;

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
        // GET: ClassNames
        public ActionResult Index()
        {
            
            return View(GetallComments());
        }
        public List<CommentVM> GetClassName()
        {
            List<CommentVM> list = new List<CommentVM>();
            list = GetallComments();
            //list = list.Where(e => e.Id == 1).ToList();
            list = list.Where(l => l.StdId==1).ToList();
            return list;
        }
        [HttpGet]
        public ActionResult Post()
        {
            ViewBag.HomeWorkId = new SelectList(new StudentsController().GetStudent(),
                "Id", "ClassName");
            ViewBag.StdId = new SelectList(new StudentsController().GetStudent(),
                "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Post(CommentVM commentVM)
        {
            using (var client = new HttpClient())
            {
               try
                {
                    
                    var result = client.PostAsJsonAsync(baseUri, commentVM).Result;
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
            return View(commentVM);
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
            CommentVM commentVM = null;
            using(var client = new HttpClient())
            {
                
                var result = client.GetAsync($"{baseUri}/{id}").Result;
                if(result.IsSuccessStatusCode)
                {
                    commentVM = result.Content.ReadAsAsync<CommentVM>().Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Edit Fail");
                }
            }
            if(commentVM == null)
            {
                return HttpNotFound();
            }
            return View(commentVM);
        }
        [HttpPost]
        public ActionResult Edit(CommentVM commentVM)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    
                    var result = client.PutAsJsonAsync(baseUri, commentVM).Result;
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
            return View(commentVM);
        }
    }
}