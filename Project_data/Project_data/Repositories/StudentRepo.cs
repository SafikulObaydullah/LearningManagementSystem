using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IStudentRepo : IRepository<Student>
    {
        Student GetByEmail(string email);
    }
    public class StudentRepo : BasicRepository<Student>,IStudentRepo
    {
        IUnitOfWork _unitOfWork;
        public StudentRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork):
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }

        public string Add(Student entity)
        {
            LmsContext.Students.Add(entity);
            return _unitOfWork.Commit();
        }

        int IRepository<Student>.Delete(int Id)
        {
           var s =  LmsContext.Students.Find(Id);
            LmsContext.Students.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        public IEnumerable<Student> GetAll()
        {
            return LmsContext.Students.ToList();
        }

        public Student GetById(int Id)
        {
            return LmsContext.Students.Find(Id);
        }

        int IRepository<Student>.update(Student entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
        public Student GetByEmail(string email)
        {
            return LmsContext.Students.Where(s => s.Email.Equals(email)).FirstOrDefault();
        }
    }
}
