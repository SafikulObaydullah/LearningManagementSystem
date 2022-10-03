using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IDesignationRepo : IRepository<Designation>
    {

    }
    public class DesignationRepo : BasicRepository<Designation>,IDesignationRepo
    {
        IUnitOfWork _unitOfWork;
        public DesignationRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork):
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }

        string IRepository<Designation>.Add(Designation entity)
        {
            LmsContext.Designations.Add(entity);
             return _unitOfWork.Commit();
        }

        int IRepository<Designation>.Delete(int Id)
        {
            var s = LmsContext.Designations.Find(Id);
            LmsContext.Designations.Remove(s);
            return 1;
        }

        public IEnumerable<Designation> GetAll()
        {
            return LmsContext.Designations.ToList();
        }

        public Designation GetById(int Id)
        {
            return LmsContext.Designations.Find(Id);
        }

        int IRepository<Designation>.update(Designation entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
