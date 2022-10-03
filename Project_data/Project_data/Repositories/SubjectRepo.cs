using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface ISubjectRepo : IRepository<Subject>
    {

    }
    public class SubjectRepo : BasicRepository<Subject>, ISubjectRepo
    {
        IUnitOfWork _unitOfWork;
        public SubjectRepo(IdatabaseFactory idatabaseFactory, IUnitOfWork unitOfWork) :
           base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }
        //public void Delete(int Id)
        //{
        //    base.Delete(LmsContext.Subjects.Find(Id));
        //}

        public IEnumerable<Subject> GetAll()
        {
            return LmsContext.Subjects.ToList();
        }
        public Subject GetById(int Id)
        {
            return LmsContext.Subjects.Find(Id);
        }

        //public string update(Subject entity)
        //{
        //    LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        //    return _unitOfWork.Commit();
        //}

        string IRepository<Subject>.Add(Subject entity)
        {
            LmsContext.Subjects.Add(entity);
            return _unitOfWork.Commit();

        }
        int IRepository<Subject>.Delete(int Id)
        {
            var s = LmsContext.Subjects.Find(Id);
            LmsContext.Subjects.Remove(s);
            LmsContext.Commit();
            return 1;
        }
        int IRepository<Subject>.update(Subject entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
        
       
    }

}
