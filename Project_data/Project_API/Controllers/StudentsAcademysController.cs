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
    public class StudentsAcademysController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public StudentsAcademysController()
        {

        }
        public string Post(StudentsAcademy studentsAcademy )
        {
            repositoryAccess.StudentsAcademyRepo.Add(studentsAcademy);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.StudentsAcademyRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<StudentsAcademy> Get()
        {
            return repositoryAccess.StudentsAcademyRepo.GetAll();
        }
        public StudentsAcademy Get(int id)
        {
            return repositoryAccess.StudentsAcademyRepo.GetById(id);
        }
        public string Put(StudentsAcademy studentsAcademy)
        {
            repositoryAccess.StudentsAcademyRepo.update(studentsAcademy);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
