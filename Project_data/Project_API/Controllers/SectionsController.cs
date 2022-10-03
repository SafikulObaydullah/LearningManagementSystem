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
    public class SectionsController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public SectionsController()
        {

        }
        public string Post(Section section)
        {
            repositoryAccess.sectionRepo.Add(section);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.sectionRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<Section> Get()
        {
            return repositoryAccess.sectionRepo.GetAll();
        }
        public Section Get(int id)
        {
            return repositoryAccess.sectionRepo.GetById(id);
        }
        public string Put(Section section)
        {
            repositoryAccess.sectionRepo.update(section);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
