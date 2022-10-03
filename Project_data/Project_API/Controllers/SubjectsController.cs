using Project_data;
using Project_data.Helper;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Project_API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SubjectsController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public SubjectsController()
        {

        }
        public string Post(Subject subject)
        {
            repositoryAccess.Subject.Add(subject);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.Subject.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<Subject> Get()
        {
            return repositoryAccess.Subject.GetAll();

        }
        public Subject Get(int id)
        {
            return repositoryAccess.Subject.GetById(id);

        }
        public string Put(Subject subject)
        {
            repositoryAccess.Subject.update(subject);
            return repositoryAccess.UnitOfWork.Commit();
        }


    }
}
