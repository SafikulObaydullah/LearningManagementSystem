using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IStudentsAcademyRepo : IRepository<StudentsAcademy>
    {

    }
    public class StudentsAcademyRepo : BasicRepository<StudentsAcademy>,IStudentsAcademyRepo
    {
        IUnitOfWork _unitOfWork;
        public StudentsAcademyRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork):
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }

        string IRepository<StudentsAcademy>.Add(StudentsAcademy entity)
        {
            LmsContext.StudentsAcademy.Add(entity);
            return _unitOfWork.Commit();
        }

        int IRepository<StudentsAcademy>.Delete(int Id)
        {
            var s = LmsContext.StudentsAcademy.Find(Id);
            LmsContext.StudentsAcademy.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        public IEnumerable<StudentsAcademy> GetAll()
        {
            return LmsContext.StudentsAcademy.ToList();
        }

        public StudentsAcademy GetById(int Id)
        {
            return LmsContext.StudentsAcademy.Find(Id);
        }
         int IRepository<StudentsAcademy>.update(StudentsAcademy entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
