using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IEmployeeRepo : IRepository<Employee>
    {

    }
    public class EmployeeRepo: BasicRepository<Employee>,IEmployeeRepo
    {
        IUnitOfWork _unitOfWork;
        public EmployeeRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork):
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }

        string IRepository<Employee>.Add(Employee entity)
        {
            LmsContext.Employes.Add(entity);
            return _unitOfWork.Commit();
        }

        int IRepository<Employee>.Delete(int Id)
        {
            var s = LmsContext.Employes.Find(Id);
            LmsContext.Employes.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        public IEnumerable<Employee> GetAll()
        {
            return LmsContext.Employes.ToList();
        }

        public Employee GetById(int Id)
        {
            return LmsContext.Employes.Find(Id);
        }

        int IRepository<Employee>.update(Employee entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
