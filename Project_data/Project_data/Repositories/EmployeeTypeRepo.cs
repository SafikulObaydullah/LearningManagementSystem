using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IEmployeeTypeRepo : IRepository<EmployeeType>
    {

    }
    public class EmployeeTypeRepo : BasicRepository<EmployeeType>, IEmployeeTypeRepo
    {
        IUnitOfWork _unitOfWork;

        public EmployeeTypeRepo(IdatabaseFactory idatabaseFactory, IUnitOfWork unitOfWork) :
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<EmployeeType> GetAll()
        {
            return LmsContext.EmployeeTypes.ToList();

        }

        public EmployeeType GetById(int Id)
        {
            return LmsContext.EmployeeTypes.Find(Id);
        }

        string IRepository<EmployeeType>.Add(EmployeeType entity)
        {
            LmsContext.EmployeeTypes.Add(entity);
            return _unitOfWork.Commit();

        }

        int IRepository<EmployeeType>.Delete(int Id)
        {
            var s = LmsContext.EmployeeTypes.Find(Id);
            LmsContext.EmployeeTypes.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        int IRepository<EmployeeType>.update(EmployeeType entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
