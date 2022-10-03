using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IClassNameRepo : IRepository<ClassName>
    {

    }
    public class ClassNameRepo : BasicRepository<ClassName>, IClassNameRepo
    {
        IUnitOfWork _unitOfWork;
        public ClassNameRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork):
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }
        string IRepository<ClassName>.Add(ClassName entity)
        {
            LmsContext.ClassNames.Add(entity);
            return _unitOfWork.Commit();
        }

        int IRepository<ClassName>.Delete(int Id)
        {
            var s = LmsContext.ClassNames.Find(Id);
            LmsContext.ClassNames.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        public IEnumerable<ClassName> GetAll()
        {
            return LmsContext.ClassNames.ToList();
        }

        public ClassName GetById(int Id)
        {
            return LmsContext.ClassNames.Find(Id);
        }

        int IRepository<ClassName>.update(ClassName entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
