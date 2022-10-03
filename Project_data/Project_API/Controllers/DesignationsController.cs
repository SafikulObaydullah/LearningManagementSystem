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
    public class DesignationsController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public DesignationsController()
        {

        }
        public string Post(Designation designation)
        {
            repositoryAccess.DesignationRepo.Add(designation);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.DesignationRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<Designation> Get()
        {
            return repositoryAccess.DesignationRepo.GetAll();
        }
        public Designation Get(int id)
        {
            return repositoryAccess.DesignationRepo.GetById(id);
        }
        public string Put(Designation designation)
        {
            repositoryAccess.DesignationRepo.update(designation);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
