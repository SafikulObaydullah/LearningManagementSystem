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
    public class GroupProgramsController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public GroupProgramsController()
        {

        }
        public string Post(GroupProgram groupProgram)
        {
            repositoryAccess.groupRepo.Add(groupProgram);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.groupRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<GroupProgram> Get()
        {
            return repositoryAccess.groupRepo.GetAll();
        }
        public GroupProgram Get(int id)
        {
            return repositoryAccess.groupRepo.GetById(id);
        }
        public string Put(GroupProgram groupProgram)
        {
            repositoryAccess.groupRepo.update(groupProgram);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
