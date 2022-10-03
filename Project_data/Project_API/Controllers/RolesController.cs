using Microsoft.AspNet.Identity.EntityFramework;
using Project_data.Repositories;
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
    public class RolesController : ApiController
    {
        RoleRepo roleRepo = new RoleRepo();
        public string Post(IdentityRole role)
        {
            return roleRepo.Create(role.Name);
        }
        public string Put(IdentityRole role)
        {
            return roleRepo.Update(role.Name, role.Id);
        }
        public string Delete(string id)
        {
            return roleRepo.Delete(id);
        }
        public IEnumerable<IdentityRole> Get()
        {
            return roleRepo.GetRoles();
        }
        public IdentityRole GetById(string id)
        {
            return roleRepo.GetByID(id);
        }
    }
}
