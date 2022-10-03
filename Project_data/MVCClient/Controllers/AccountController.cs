using MVCClient.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MVCClient.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login(string returnURL)
        {
            ViewBag.ReturnUrl = returnURL;
            return View();
        }
        public IEnumerable<Claim> GetClaim(string token)
        {
            IEnumerable<Claim> claim = null;
            if(token != null)
            {
                token = Session["token"].ToString();
                var securityTokenHandler = new JwtSecurityTokenHandler();
                if(securityTokenHandler.CanReadToken(token))
                {
                    var decriptedToken = securityTokenHandler.ReadJwtToken(token);
                    claim = decriptedToken.Claims;
                }
            }
            return claim;
        }
        [HttpPost]
        public ActionResult Login(LoginVM loginVM,string returnURL)
        {
            var tokenBased = string.Empty;
            using (var client = new HttpClient())
            {
                string baseUri = "";
                try
                {
                    baseUri = "https://localhost:44372/api/Users";
                    var jsonSerializerSettings = new JsonSerializerSettings
                    {
                        StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
                    };
                    string JsonTest = JsonConvert.SerializeObject(loginVM, jsonSerializerSettings);

                    var result = client.GetAsync(baseUri + "/GetValidLogin?loginVM=" + JsonTest).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        //Session['u']=
                        var s = User.Identity.IsAuthenticated;
                        var output = result.Content.ReadAsStringAsync().Result;
                        tokenBased = JsonConvert.DeserializeObject<string>(output);
                        Session["token"] = tokenBased;
                        var principal = GetClaim(tokenBased);
                        var customClaimValue = principal.Where(c => c.Type == "Roles").Single().Value;
                        var email = principal.Where(c => c.Type == "Email").Single().Value;
                        if (returnURL != null)
                        {
                            var urls = returnURL.Split('/');
                            var c = urls[0].ToString();
                            var a = urls[1].ToString();
                            Session["std"] = GetStdDetails(email);
                            return RedirectToAction(a, c);
                        }
                        else
                        {

                            var U = HttpContext.User.Identity.Name;

                            if (customClaimValue.ToLower() == "admin"
                                || customClaimValue.ToLower() == "hr")
                            {
                                return RedirectToAction("Index", "Admin");
                            }
                            if (customClaimValue == "Student")
                            {
                                var std = GetStdDetails(email);
                                if (std != null)
                                {
                                    System.Web.HttpContext.Current.Session["std"] = std;
                                    return RedirectToAction("Index", "StudentsAcademys");
                                }
                                else
                                {
                                    return RedirectToAction("NotFound", "StudentsAcademys");

                                }
                            }


                        }

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

            return View(loginVM);

        }

        public ActionResult LogOff()
        {
            System.Web.HttpContext.Current.Session["token"] = null;
            return RedirectToAction("Index", "Home");
        }

        private StudentVM GetStdDetails(string email)
        {

            if (System.Web.HttpContext.Current.Session["token"] != null)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        string baseUri = "https://localhost:44372/api/Students";
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(scheme: "Bearer", parameter: Session["token"].ToString());
                        var result = client.GetAsync($"{baseUri}?email={email}").Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var std = result.Content.ReadAsAsync<StudentVM>().Result;
                            return std;
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
            return new StudentVM();

        }


        public ActionResult UserProfile()
        {
            if (System.Web.HttpContext.Current.Session["token"] != null)
            {
                ViewBag.token = System.Web.HttpContext.Current.Session["token"];
            }
            return View();
        }
    }
}