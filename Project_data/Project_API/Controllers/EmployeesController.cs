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
    public class EmployeesController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public EmployeesController()
        {

        }
        public string Post(Employee employee)
        {
            repositoryAccess.EmployeeRepo.Add(employee);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.EmployeeRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<Employee> Get()
        {
            return repositoryAccess.EmployeeRepo.GetAll();
        }
        public Employee Get(int id)
        {
            return repositoryAccess.EmployeeRepo.GetById(id);
        }
        public string Put(Employee employee)
        {
            repositoryAccess.EmployeeRepo.update(employee);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
