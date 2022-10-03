using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface ISemesterRepo : IRepository<Semester>
    {

    }
    public class SemesterRepo : BasicRepository<Semester>, ISemesterRepo
    {
        IUnitOfWork _unitOfWork1;
        public SemesterRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork):
            base(idatabaseFactory)
        {
            _unitOfWork1 = unitOfWork;
        }
        public string Add(Semester entity)
        {
            LmsContext.Semesters.Add(entity);
            return _unitOfWork1.Commit();
        }

       int IRepository<Semester>.Delete(int Id)
        {
            var s = LmsContext.Semesters.Find(Id);
            LmsContext.Semesters.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        public IEnumerable<Semester> GetAll()
        {
            return LmsContext.Semesters.ToList();
        }

        public Semester GetById(int Id)
        {
           return LmsContext.Semesters.Find(Id);
        }

        int IRepository<Semester>.update(Semester entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork1.Commit();
            return 1;
        }
    }
}
