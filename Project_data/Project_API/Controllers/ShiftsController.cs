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
    public class ShiftsController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        
        public ShiftsController()
        {

        }
        public string Post(Shift shift)
        {
            repositoryAccess.Shift.Add(shift);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.Shift.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<Shift> Get()
        {
            return repositoryAccess.Shift.GetAll();
        }
        public Shift Get(int id)
        {
            return repositoryAccess.Shift.GetById(id);
        }
        public string Put(Shift shift)
        {
            repositoryAccess.Shift.update(shift);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
