using MVCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;



namespace MVCClient.Controllers
{
    public class SubmittedHomeworksController : Controller
    {
        string baseUri = "https://localhost:44372/api/SubmittedHomeworks";
        // GET: SubmittedHomeworks
        public ActionResult Index()
        {
            List<SubmittedHomeworkVM> list = new List<SubmittedHomeworkVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<SubmittedHomeworkVM>>().Result;
                        return View(list);
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
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<HomeworkVM> list = new List<HomeworkVM>();
            
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/HomeWorks/";
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<HomeworkVM>>().Result;
                    }
                }
                catch(Exception ex)
                {

                }
            }

            ViewBag.HomeworkID = new SelectList(list.OrderBy(l => l.PublishedDate).ToList(), "Id", "PublishedDate");

            List<StudentVM> stuList = new List<StudentVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    string baseUri = "https://localhost:44372/api/Students/";
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        
                        stuList = result.Content.ReadAsAsync<List<StudentVM>>().Result;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            ViewBag.StudentID = new SelectList(stuList.OrderBy(l => l.Name).ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SubmittedHomeworkVM submittedHomeworkVM, HttpPostedFileBase[] HomeworkFiles, string homeworkStatus)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    submittedHomeworkVM.HomeworkID = int.Parse(this.RouteData.Values["id"].ToString());
                    var std = (System.Web.HttpContext.Current.Session["std"] as StudentVM);
                    submittedHomeworkVM.StudentID = std.Id;
                    var result = client.PostAsJsonAsync(baseUri, submittedHomeworkVM).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var homework = result.Content.ReadAsAsync<SubmittedHomeworkVM>().Result;
                        using (var content = new MultipartFormDataContent())
                        {
                            for (int i = 0; i < HomeworkFiles.Count(); i++)
                            {
                                MemoryStream target = new MemoryStream();
                                HomeworkFiles[i].InputStream.CopyTo(target);
                                byte[] Bytes = target.ToArray();
                                HomeworkFiles[i].InputStream.Read(Bytes, 0, Bytes.Length);
                                var fileContent = new ByteArrayContent(Bytes);
                                fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                                { FileName = HomeworkFiles[i].FileName };
                                content.Add(fileContent);
                            }
                            content.Add(new StringContent(homework.Id.ToString()), "SubmittedHomeworkId");
                            content.Headers.Add("enctype", "multipart/form-data");

                            //content.Headers.ContentType= new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                            //content.Headers.Add("Key", "abc23sdflsdf");
                            var requestUri = "https://localhost:44372/api/HomeWorkFiles/";
                            var fileresult = client.PostAsync(requestUri, content).Result;
                            //if (fileresult.StatusCode == System.Net.HttpStatusCode.Created)
                            if (fileresult.IsSuccessStatusCode)
                            {
                                HomeworkVM homeworkVM = new HomeworkVM();
                                homeworkVM.Id = int.Parse(this.RouteData.Values["id"].ToString());
                                homeworkVM.Status = HomeworkStatus.Submitted;
                                var r = client.PutAsJsonAsync("https://localhost:44372/api/HomeWorks", homeworkVM);

                                ViewBag.Success = fileresult.ReasonPhrase;
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.Failed = "Failed !" + fileresult.ReasonPhrase.ToString();
                            }
                        }

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

            return View(submittedHomeworkVM);
        }
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
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
            SubmittedHomeworkVM submittedHomeworkVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        submittedHomeworkVM = result.Content.ReadAsAsync<SubmittedHomeworkVM>().Result;
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
            if (submittedHomeworkVM == null)
            {
                return HttpNotFound();
            }

            return View(submittedHomeworkVM);
        }
        [HttpPost]
        public ActionResult Edit(SubmittedHomeworkVM submittedHomeworkVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.PutAsJsonAsync(baseUri, submittedHomeworkVM).Result;
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
            return View(submittedHomeworkVM);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubmittedHomeworkVM submittedHomeworkVM = null;

            using (var client = new HttpClient())
            {
                try
                {

                    var result = client.GetAsync($"{baseUri}/{id}").Result;

                    if (result.IsSuccessStatusCode)
                    {
                        submittedHomeworkVM = result.Content.ReadAsAsync<SubmittedHomeworkVM>().Result;
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
                finally
                {

                }
            }
            if (submittedHomeworkVM == null)
            {
                return HttpNotFound();
            }

            return View(submittedHomeworkVM);
        }
    }
}