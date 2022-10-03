using Project_data;
using Project_data.Helper;
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
    public class InstituteTypesController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public InstituteTypesController()
        {

        }
        public string Post(InstituteType instituteType)
        {
            repositoryAccess.InstituteTypeRepo.Add(instituteType);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.InstituteTypeRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<InstituteType> Get()
        {
            return repositoryAccess.InstituteTypeRepo.GetAll();
        }
        public InstituteType Get(int id)
        {
            return repositoryAccess.InstituteTypeRepo.GetById(id);
        }
        public string Put(InstituteType instituteType)
        {
            repositoryAccess.InstituteTypeRepo.update(instituteType);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
