using MVCClient.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class StudentsController : Controller
    {
        string baseUri = "https://localhost:44372/api/Students";
        private List<StudentVM> GetallStudents()
        {
            List<StudentVM> list = new List<StudentVM>();
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<StudentVM>>().Result;

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
        // GET: Students
        public ActionResult Index()
        {
            return View(GetallStudents());
        }
        public List<StudentVM> GetStudent()
        {
            List<StudentVM> list = new List<StudentVM>();
            list = GetallStudents();
            //list = list.Where(e => e.TypeID == 1).ToList();
            list = list.OrderBy(l => l.Name).ToList();
            return list;
        }

        [HttpGet]
        public ActionResult Create()
        {
         ViewBag.InstId = new SelectList(new InstituteInfosController().GetInstituteInfo(),
             "Id", "InstituteName");
         return View();
      }
        [HttpPost]
      public ActionResult Create(StudentVM studentVM)
      {
         using (var client = new HttpClient())
         {
            try
            {
               if (studentVM.StudentPicture != null)
               {
                  string fPath = Path.Combine(Server.MapPath("~/"), "Images/StdPic");
                  if (!Directory.Exists(fPath))
                  {
                     Directory.CreateDirectory(fPath);
                  }
                  string ext = Path.GetExtension(studentVM.StudentPicture.FileName);
                  if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg" || ext.ToLower() == ".png" || ext.ToLower() == ".gif")
                  {
                     string fName = studentVM.Name + ext;
                     string filetoSave = Path.Combine(fPath, fName);
                     using (var content = new MultipartFormDataContent())
                     {
                        MemoryStream target = new MemoryStream();
                        studentVM.StudentPicture.InputStream.CopyTo(target);
                        byte[] Bytes = target.ToArray();
                        studentVM.StudentPicture.InputStream.Read(Bytes, 0, Bytes.Length);
                        var fileContent = new ByteArrayContent(Bytes);
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                        { FileName = studentVM.StudentPicture.FileName };
                        content.Add(fileContent);
                        content.Add(new StringContent(studentVM.Name), "Name");
                        content.Add(new StringContent(studentVM.Gender.ToString()), "Gender");
                        content.Add(new StringContent(studentVM.ContactNo), "ContactNo");
                        content.Add(new StringContent(studentVM.Email), "Email");
                        content.Add(new StringContent(studentVM.DOB.ToString()), "DOB");
                        content.Add(new StringContent(studentVM.InstId.ToString()), "InstId");
                        var result = client.PostAsync(baseUri, content).Result;
                        var c = result.StatusCode;
                        if (result.IsSuccessStatusCode)
                        {
                           return RedirectToAction("Index");
                        }
                        else
                        {
                           ModelState.AddModelError("", "Save failed");
                        }
                     }
                  }
                  else
                  {
                     ModelState.AddModelError("", "Please Provide Valid Picture");
                  }

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


         return View(studentVM);
         //ViewBag.InstId = new SelectList(new InstituteInfosController().GetInstituteInfo(),
         //       "Id", "InstituteName");
      }
      public ActionResult Delete(int id)
        {
            using(var client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage httpResponseMessage = client.DeleteAsync($"{baseUri}/{id}").Result;
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
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
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            StudentVM studentVM = null;
            using(var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if(result.IsSuccessStatusCode)
                    {
                        
                        studentVM = result.Content.ReadAsAsync<StudentVM>().Result;
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(studentVM);
        }
        [HttpPost]
        public ActionResult Edit(StudentVM studentVM)
        {
            using (var client = new HttpClient())
            {
                try
                {
                   
                    var result = client.PutAsJsonAsync(baseUri, studentVM).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Update fail");
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
            return View(studentVM);
            
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            StudentVM studentVM = null;
            using (var client = new HttpClient())
            {
                try
                {
                    var result = client.GetAsync($"{baseUri}/{id}").Result;
                    if (result.IsSuccessStatusCode)
                    {

                        studentVM = result.Content.ReadAsAsync<StudentVM>().Result;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(studentVM);
        }

    }
}