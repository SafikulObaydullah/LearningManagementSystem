using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using Project_data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface IHomeWorkRepo : IRepository<Homework>
    {
        IEnumerable<HomeworkVM> GetAllByStd(int classId, int secId, int shiftId);
    }
    public class HomeWorkRepo : BasicRepository<Homework>, IHomeWorkRepo
    {
        IUnitOfWork _unitOfWork;
        public HomeWorkRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork) : 
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Homework> GetAll()
        {
            return LmsContext.Homeworks.ToList();

        }

        public IEnumerable<HomeworkVM> GetAllByStd(int classId, int secId, int shiftId)
        {
            return LmsContext.Homeworks.Where(h => h.SecID.Equals(secId) &&
               h.ClassId.Equals(classId) && h.ShiftId.Equals(shiftId)

           ).Select(s => new HomeworkVM
           {
               SubjectName = s.Subject.SubName,
               Teacher = s.Employee.Name,
               PublishedDate = s.PublishedDate,
               DueDate = s.DueDate,
               Title = s.Title,
               Description = s.Description,
               Id = s.Id
           }).ToList();
        }

        public Homework GetById(int Id)
        {
            return LmsContext.Homeworks.Find(Id);
        }

        string IRepository<Homework>.Add(Homework entity)
        {
            LmsContext.Homeworks.Add(entity);
            return _unitOfWork.Commit();

        }

        int IRepository<Homework>.Delete(int Id)
        {
            var s = LmsContext.Homeworks.Find(Id);
            LmsContext.Homeworks.Remove(s);
            _unitOfWork.Commit();
            return 1;
        }

        int IRepository<Homework>.update(Homework entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
    }
}
