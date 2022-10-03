using Project_data;
using Project_data.Helper;
using Project_data.ViewModels;
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
    public class HomeWorksController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public HomeWorksController()
        {

        }
        public string Post(Homework homework)
        {
            repositoryAccess.HomeworkRepo.Add(homework);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.HomeworkRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<Homework> Get()
        {
            return repositoryAccess.HomeworkRepo.GetAll();
        }
        public IEnumerable<HomeworkVM> GetBYStd(int classId, int secId, int shiftId)
        {
            return repositoryAccess.HomeworkRepo.GetAllByStd(classId, secId, shiftId);
        }
        public Homework Get(int id)
        {
           return repositoryAccess.HomeworkRepo.GetById(id); 
        }
        public string Put(Homework homework)
        {
            repositoryAccess.HomeworkRepo.update(homework);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
