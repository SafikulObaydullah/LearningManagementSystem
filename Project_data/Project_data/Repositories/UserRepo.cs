using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project_data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public class UserRepo
    {
        DbLMSContext db = new DbLMSContext();
        //public UserRepo(IdatabaseFactory idatabaseFactory) 
        //{
        //    db = idatabaseFactory.Get();
        //}

        UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new DbLMSContext()));
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DbLMSContext()));
        public string Create(UserVM userVM)
        {
            string msg = "";
            IdentityResult result = null;
            var user = new ApplicationUser
            {
                //IdentityUser  //ApplicationUser
                UserName = userVM.UserName,
                Email = userVM.Email,
                //PhoneNumber = userVM.PhoneNumber
            };
            try
            {
                result = userManager.Create(user, userVM.Password);
                if (result.Succeeded)
                {
                    result = userManager.AddToRole(user.Id, userVM.RolesName);
                    if (result.Succeeded)
                    {
                        msg = $"{user.UserName} created successfully";
                    }
                    else if (result.Errors.Count() > 0)
                    {
                        msg = DisplayError(msg, result);
                    }
                }
                else if (result.Errors.Count() > 0)
                {
                    msg = DisplayError(msg, result);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        public UserVM GetByID(string id)
        {
            var user = userManager.FindById(id);
            var rNames = userManager.GetRoles(id).FirstOrDefault();
            UserVM userVM = new UserVM();
            userVM.Email = user.Email;
            userVM.UserID = user.Id;
            userVM.UserName = user.UserName;
            userVM.RolesName = rNames;
            return userVM;
        }

        private static string DisplayError(string msg, IdentityResult roleresult)
        {
            foreach (var er in roleresult.Errors)
            {
                msg = er;
            }
            return msg;
        }
        public string Update(UserVM userVM)
        {
            string msg = "";
            IdentityResult result = null;
            var user = userManager.FindById(userVM.UserID);
            if (user != null)
            {
                user.Email = userVM.Email;
                user.UserName = userVM.UserName;
                //PhoneNumber = userVM.PhoneNumber

            }
            try
            {
                result = userManager.Update(user);
                if (result.Succeeded)
                {
                    var usroles = userManager.GetRoles(user.Id).ToArray();
                    userManager.RemoveFromRoles(user.Id, usroles);
                    result = userManager.AddToRole(user.Id, userVM.RolesName);
                    if (result.Succeeded)
                    {
                        msg = $"{user.UserName} updated successfully";
                    }
                    else if (result.Errors.Count() > 0)
                    {
                        msg = DisplayError(msg, result);
                    }
                }
                else if (result.Errors.Count() > 0)
                {
                    msg = DisplayError(msg, result);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        public string Delete(string userId)
        {
            string msg = "";
            IdentityResult result = null;
            var user = userManager.FindById(userId);
            if (user != null)
            {

            }
            try
            {
                result = userManager.Delete(user);
                if (result.Succeeded)
                {
                    msg = $"{user.UserName} deleted successfully";
                }
                else if (result.Errors.Count() > 0)
                {
                    msg = DisplayError(msg, result);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        public List<UserVM> AllUser(int? pageNo, string roleName, string email, string currentFilter)
        {
            List<UserVM> usersWithRoles = new List<UserVM>();

            int noOfPages = pageNo ?? 1;
            usersWithRoles = (from user in db.Users
                              select new
                              {
                                  UserId = user.Id,
                                  Username = user.UserName,
                                  Email = user.Email,
                                  RoleNames = (from userRole in user.Roles
                                               join role in db.Roles on userRole.RoleId
                                               equals role.Id
                                               select role.Name).ToList()
                              }).ToList().Select(p => new UserVM()

                              {
                                  UserID = p.UserId,
                                  UserName = p.Username,
                                  Email = p.Email,
                                  RolesName = string.Join(",", p.RoleNames)
                              }).OrderBy(r => r.UserName).ToList();

            if (roleName == null)
            {
                roleName = "";
            }
            if (email == null)
            {
                email = "";
            }
            if (email != null)
            {
                pageNo = 1;
            }
            else
            {
                email = currentFilter;

            }

            if (roleName != string.Empty)
            {
                usersWithRoles = usersWithRoles.Where(u => u.RolesName.Contains(roleName)).ToList();
            }
            if (email != string.Empty || email.Length > 0)
            {
                usersWithRoles = usersWithRoles.Where(u => u.Email == email).ToList();

            }
            return usersWithRoles;
        }

        public List<UserVM> AllUser()
        {
            List<UserVM> usersWithRoles = new List<UserVM>();
            usersWithRoles = (from user in db.Users
                              select new
                              {
                                  UserId = user.Id,
                                  Username = user.UserName,
                                  Email = user.Email,
                                  RoleNames = (from userRole in user.Roles
                                               join role in db.Roles on userRole.RoleId
                                               equals role.Id
                                               select role.Name).ToList()
                              }).ToList().Select(p => new UserVM()

                              {
                                  UserID = p.UserId,
                                  UserName = p.Username,
                                  Email = p.Email,
                                  RolesName = string.Join(",", p.RoleNames)
                              }).OrderBy(r => r.UserName).ToList();
            return usersWithRoles;
        }
        public UserVM SignIn(LoginVM loginVM)
        {
            var user1 = userManager.Find(loginVM.UserName, loginVM.Password);
            var user = db.Users.Where(u => u.UserName == loginVM.UserName).FirstOrDefault();
            if (user != null)
            {
                var roles = userManager.GetRoles(user.Id);
                UserVM loggedUser = new UserVM
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    RolesName = roles.FirstOrDefault(),
                    UserID = user.Id
                };
                return loggedUser;
            }
            return new UserVM();

        }

    }
}
