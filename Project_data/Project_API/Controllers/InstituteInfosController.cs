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
    public class InstituteInfosController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public InstituteInfosController()
        {

        }
        public string Post(InstituteInfo instituteInfo)
        {
            repositoryAccess.InstituteInfoRepo.Add(instituteInfo);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.InstituteInfoRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<InstituteInfo> Get()
        {
            return repositoryAccess.InstituteInfoRepo.GetAll();
        }
        public InstituteInfo Get(int id)
        {
            return repositoryAccess.InstituteInfoRepo.GetById(id);
        }
        public string Put(InstituteInfo instituteInfo)
        {
            repositoryAccess.InstituteInfoRepo.update(instituteInfo);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
