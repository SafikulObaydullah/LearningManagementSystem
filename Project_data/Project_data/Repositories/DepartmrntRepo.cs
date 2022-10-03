using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IDepartmentRepo : IRepository<Department>
    {

    }
    public class DepartmrntRepo : BasicRepository<Department>,IDepartmentRepo
    {
        IUnitOfWork _unitOfWork;
        public DepartmrntRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork) :
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }

        public string Add(Department entity)
        {
            LmsContext.Departments.Add(entity);
            return _unitOfWork.Commit();
        }

        int IRepository<Department>.Delete(int Id)
        {
            var s = LmsContext.Departments.Find(Id);
            LmsContext.Departments.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        public IEnumerable<Department> GetAll()
        {
            return LmsContext.Departments.ToList();
        }

        public Department GetById(int Id)
        {
            return LmsContext.Departments.Find(Id);
        }

        public int update(Department entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
