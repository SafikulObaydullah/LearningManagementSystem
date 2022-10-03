using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IInstituteInfoRepo : IRepository<InstituteInfo>
    {

    }

    public class InstituteInfoRepo : BasicRepository<InstituteInfo>,IInstituteInfoRepo
    {
        IUnitOfWork _unitOfWork;
        public InstituteInfoRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork):
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }

        string IRepository<InstituteInfo>.Add(InstituteInfo entity)
        {
            LmsContext.InstituteInfos.Add(entity);
            return _unitOfWork.Commit();
        }

        int IRepository<InstituteInfo>.Delete(int Id)
        {
            var s = LmsContext.InstituteInfos.Find(Id);
            LmsContext.InstituteInfos.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        public IEnumerable<InstituteInfo> GetAll()
        {
            return LmsContext.InstituteInfos.ToList();
        }

        public InstituteInfo GetById(int Id)
        {
            return LmsContext.InstituteInfos.Find(Id);
        }

        int IRepository<InstituteInfo>.update(InstituteInfo entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
