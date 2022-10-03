using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public class RoleRepo
    {
        DbLMSContext db = new DbLMSContext();
        RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DbLMSContext()));
        public string Create(string rname)
        {
            string msg = "";
            try
            {
                IdentityResult roleresult = null;
                roleresult = roleManager.Create(new IdentityRole { Name = rname });
                if (roleresult.Succeeded)
                {
                    msg = $"{rname} created successfully";
                }
                else if (roleresult.Errors.Count() > 0)
                {
                    msg = DisplayError(msg, roleresult);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        private static string DisplayError(string msg, IdentityResult roleresult)
        {
            foreach (var er in roleresult.Errors)
            {
                msg = er;
            }
            return msg;
        }
        public IEnumerable<IdentityRole> GetRoles()
        {
            return roleManager.Roles.ToList();
        }
        public IdentityRole GetByID(string id)
        {
            return roleManager.FindById(id);
        }
        public string Update(string rname, string id)
        {
            string msg = "";
            try
            {
                IdentityResult roleresult = null;
                roleresult = roleManager.Update(new IdentityRole { Name = rname, Id = id });
                if (roleresult.Succeeded)
                {
                    msg = $"{rname} updated successfully";
                }
                else if (roleresult.Errors.Count() > 0)
                {
                    msg = DisplayError(msg, roleresult);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }

        public string Delete(string id)
        {
            string msg = "";
            try
            {
                IdentityRole updatableRoles = roleManager.FindById(id);
                IdentityResult roleresult = null;
                roleresult = roleManager.Delete(updatableRoles);
                if (roleresult.Succeeded)
                {
                    msg = $"{updatableRoles.Name} Deleted successfully";
                }
                else if (roleresult.Errors.Count() > 0)
                {
                    msg = DisplayError(msg, roleresult);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
    }
}
