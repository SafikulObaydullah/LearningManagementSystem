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
    public class SemestersController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        
        public SemestersController()
        {

        }
        public string Post(Semester semester)
        {
            repositoryAccess.semesterRepo.Add(semester);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.semesterRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<Semester> Get()
        {
            return repositoryAccess.semesterRepo.GetAll();
        }
        public Semester Get(int id)
        {
            return repositoryAccess.semesterRepo.GetById(id);
        }
        public string Put(Semester semester)
        {
            repositoryAccess.semesterRepo.update(semester);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
