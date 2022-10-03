using Newtonsoft.Json;
using Project_API.Authorization;
using Project_data;
using Project_data.Repositories;
using Project_data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Project_API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UsersController : ApiController
    {
        UserRepo userRepo = new UserRepo();
        public string Post(UserVM user)
        {
            //return userRepo.Create(user.UserName,user.Password);
            return userRepo.Create(user);
        }
        public string Put(UserVM user)
        {
            return userRepo.Update(user);
        }
        public string Delete(string id)
        {
            return userRepo.Delete(id);
        }
        public IEnumerable<UserVM> Get(int? pageNo, string roleName, string email, string currentFilter)
        {
            return userRepo.AllUser(pageNo, roleName, email, currentFilter);
        }
        public IEnumerable<UserVM> Get()
        {
            var s = User.Identity.IsAuthenticated;
            return userRepo.AllUser();
        }
        //public UserVM GetById(string id)
        //{
        //    return userRepo.GetByID(id);
        //}
        public HttpResponseMessage GetValidLogin(string loginVM)
        {
            var u = JsonConvert.DeserializeObject<LoginVM>(loginVM);

            var user = userRepo.SignIn(u);
            if (user != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(user));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Username or Password");
            }

        }
    }
}
