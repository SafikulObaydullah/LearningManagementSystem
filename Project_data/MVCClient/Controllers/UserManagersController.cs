using Microsoft.AspNet.Identity;
using MVCClient.ViewModels;
using Newtonsoft.Json;
using Project_data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class UserManagersController : Controller
    {
        string baseUri = "https://localhost:44372/api/Users/";
        public UserVM GetUser(string id)
        {
            UserVM user = new UserVM();
            using (var client = new HttpClient())
            {
                string baseUri = "";
                try
                {
                    if (id != string.Empty)
                    {
                        baseUri = "https://localhost:44372/api/Users/" + id;
                    }
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        user = result.Content.ReadAsAsync<UserVM>().Result;
                        return user;
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
            return user;
        }


        public List<UserVM> GetUsers()
        {
            List<UserVM> list = new List<UserVM>();
            using (var client = new HttpClient())
            {
                string baseUri = "";
                try
                {

                    baseUri = "https://localhost:44372/api/Users";
                    var result = client.GetAsync(baseUri).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        list = result.Content.ReadAsAsync<List<UserVM>>().Result;
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
            return list;
        }
        // GET: UserManagers
        public ActionResult Index()
        {
            ViewBag.ReturnUrl = "";
            if (Session["token"] != null)
            {
                return View(GetUsers());
            }
            else
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnURL = "UserManagers/Index"

                });
            }

        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["token"] != null)
            {
                ViewBag.RolesName = new SelectList(new RolesController().GetRoles(), "Name", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnURL = "UserManagers/Index"

                });
            }

        }
        [HttpPost]
        public ActionResult Create(UserVM userVM)
        {
            if (Session["token"] != null)
            {

                ViewBag.RolesName = new SelectList(new RolesController().GetRoles(), "Name", "Name");
                using (var client = new HttpClient())
                {
                    try
                    {
                        //string baseUri = "https://localhost:44327/api/Users/";
                        var result = client.PostAsJsonAsync(baseUri, userVM).Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("Savefail");
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
            else
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnURL = "UserManagers/Index"

                });
            }

        }

        [HttpGet]
        public ActionResult Edit(string Id)
        {
            if (Session["token"] != null)
            {
                var users = GetUser(Id);
                ViewBag.RolesName = new SelectList(new RolesController().GetRoles(), "Name", "Name", users.RolesName);
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnURL = "UserManagers/Index"
                });
            }

        }
        [HttpPost]
        public ActionResult Edit(UserVM userVM)
        {
            if (Session["token"] != null)
            {

                ViewBag.RolesName = new SelectList(new RolesController().GetRoles(), "Name", "Name");
                using (var client = new HttpClient())
                {
                    try
                    {
                        //string baseUri = "https://localhost:44327/api/Users/";
                        var result = client.PutAsJsonAsync(baseUri, userVM).Result;
                        if (result.IsSuccessStatusCode)
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
            else
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnURL = "UserManagers/Index"

                });
            }

        }
        public ActionResult Details(string Id)
        {
            if (Session["token"] != null)
            {
                var users = GetUser(Id);
                ViewBag.RolesName = new SelectList(new RolesController().GetRoles(), "Name", "Name", users.RolesName);
                return View(users);
            }
            else
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnURL = "UserManagers/Index"
                });
            }

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult UserProfile()
        {
            if (Session["token"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account", new
                {
                    returnURL = "UserManagers/Index"

                });
            }
        }
        [HttpPost]
        public ActionResult Login(UserVM userVM)
        {
            //userVM.UserName = "Obaidullah";
            //userVM.Password = "@A123#";

            using (var client = new HttpClient())
            {
                //string baseUri = "";
                try
                {
                    //baseUri = "https://localhost:44327/api/Users";
                    var jsonSerializerSettings = new JsonSerializerSettings
                    {
                        StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                    };

                    string JsonTest = JsonConvert.SerializeObject(userVM, jsonSerializerSettings);

                    var result = client.GetAsync(baseUri + "/?userVm=" + JsonTest).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //Session['u']=
                        var s = User.Identity.IsAuthenticated;

                        return RedirectToAction("UserProfile");
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
            return View(userVM);
        }

    }
}