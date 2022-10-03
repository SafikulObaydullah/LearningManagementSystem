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
    public class EmployeeTypesController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public EmployeeTypesController()
        {
            
        }
        public string Post(EmployeeType employeeType)
        {
            repositoryAccess.EmployeeTypeRepo.Add(employeeType);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.EmployeeTypeRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<EmployeeType> Get()
        {
            return repositoryAccess.EmployeeTypeRepo.GetAll();
        }
        public EmployeeType Get(int id)
        {
           return repositoryAccess.EmployeeTypeRepo.GetById(id);
        }
        public string Put(EmployeeType employeeType)
        {
            repositoryAccess.EmployeeTypeRepo.update(employeeType);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
