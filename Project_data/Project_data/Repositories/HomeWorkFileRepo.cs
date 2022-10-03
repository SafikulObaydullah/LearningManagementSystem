using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IHomeWorkFileRepo : IRepository<HomeWorkFile>
    {

    }
    public class HomeWorkFileRepo : BasicRepository<HomeWorkFile>, IHomeWorkFileRepo
    {
        IUnitOfWork _unitOfWork;
        public HomeWorkFileRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork) : 
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<HomeWorkFile> GetAll()
        {
            return LmsContext.HomeWorkFiles.ToList();

        }

        public HomeWorkFile GetById(int Id)
        {
            return LmsContext.HomeWorkFiles.Find(Id);
        }

        string IRepository<HomeWorkFile>.Add(HomeWorkFile entity)
        {
            LmsContext.HomeWorkFiles.Add(entity);
            return _unitOfWork.Commit();

        }

        int IRepository<HomeWorkFile>.Delete(int Id)
        {
            var s = LmsContext.HomeWorkFiles.Find(Id);
            LmsContext.HomeWorkFiles.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        int IRepository<HomeWorkFile>.update(HomeWorkFile entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
