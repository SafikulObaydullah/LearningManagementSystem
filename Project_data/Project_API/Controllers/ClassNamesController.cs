using Project_API.Authorization;
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
    [CustomAuthenticationFilter]
    [EnableCors("*", "*", "*")]
    public class ClassNamesController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public ClassNamesController()
        {

        }
        public string Post(ClassName className)
        {
            repositoryAccess.classNameRepo.Add(className);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.classNameRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        //[CustomAuthenticationFilter]
        public IEnumerable<ClassName> Get()
        {
           return repositoryAccess.classNameRepo.GetAll();
        }
        public ClassName Get(int id)
        {
            return repositoryAccess.classNameRepo.GetById(id);
        }
        public string Put(ClassName className)
        {
            repositoryAccess.classNameRepo.update(className);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
