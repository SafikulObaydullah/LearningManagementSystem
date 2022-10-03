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
    public class SubmittedHomeworksController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public SubmittedHomeworksController()
        {

        }
        //public string Post(SubmittedHomework submittedHomework)
        //{
        //    repositoryAccess.SubmittedHomeworkRepo.Add(submittedHomework);
        //    return repositoryAccess.UnitOfWork.Commit();
        //}
        public SubmittedHomework Post(SubmittedHomework shw)
        {
            shw.SubmittedDate = DateTime.Now;
            //  repositoryAccess.SubmittedHomeworkRepo.Add(shw);
            //return  repositoryAccess.UnitofWork.Commit();


            return repositoryAccess.SubmittedHomeworkRepo.Insert(shw);
        }
        public string Delete(int id)
        {
            repositoryAccess.SubmittedHomeworkRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<SubmittedHomework> Get()
        {
            return repositoryAccess.SubmittedHomeworkRepo.GetAll();
        }
        public SubmittedHomework Get(int id)
        {
            return repositoryAccess.SubmittedHomeworkRepo.GetById(id);
        }
        public string Put(SubmittedHomework submittedHomework)
        {
            repositoryAccess.SubmittedHomeworkRepo.update(submittedHomework);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
