using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IGroupRepo : IRepository<GroupProgram>
    {

    }
    public class GroupProgramRepo : BasicRepository<GroupProgram>,IGroupRepo 
    {
        IUnitOfWork _unitOfWork;
        public GroupProgramRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork) :
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }

        string IRepository<GroupProgram>.Add(GroupProgram entity)
        {
            LmsContext.GroupPrograms.Add(entity);
            return _unitOfWork.Commit();
        }

        int IRepository<GroupProgram>.Delete(int Id)
        {
            var s = LmsContext.GroupPrograms.Find(Id);
            LmsContext.GroupPrograms.Remove(s);
            LmsContext.Commit();
            return 1;

        }

        public IEnumerable<GroupProgram> GetAll()
        {
           return LmsContext.GroupPrograms.ToList();
        }

        public GroupProgram GetById(int Id)
        {
            return LmsContext.GroupPrograms.Find(Id);
        }

        int IRepository<GroupProgram>.update(GroupProgram entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
