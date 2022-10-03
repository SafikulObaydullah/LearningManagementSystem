using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IInstituteType : IRepository<InstituteType>
    {

    }
    public class InstituteTypeRepo:BasicRepository<InstituteType>,IInstituteType
    {
        IUnitOfWork _unitOfWork;
        public InstituteTypeRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork):
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }

        string IRepository<InstituteType>.Add(InstituteType entity)
        {
            LmsContext.InstituteType.Add(entity);
            return _unitOfWork.Commit();
        }

        int IRepository<InstituteType>.Delete(int Id)
        {
            var s = LmsContext.InstituteType.Find(Id);
            LmsContext.InstituteType.Remove(s);
            return 1;
        }

        public IEnumerable<InstituteType> GetAll()
        {
            return LmsContext.InstituteType.ToList();
        }

        public InstituteType GetById(int Id)
        {
            return LmsContext.InstituteType.Find(Id);
        }

        int IRepository<InstituteType>.update(InstituteType entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
