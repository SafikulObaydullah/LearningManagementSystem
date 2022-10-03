using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface ISubmittedHomeworkRepo : IRepository<SubmittedHomework>
    {
        SubmittedHomework Insert(SubmittedHomework entity);
        bool IsSubmiited(int hwId);
    }

    public class SubmittedHomeworkRepo : BasicRepository<Section>, ISubmittedHomeworkRepo
    {
        IUnitOfWork _unitOfWork;

        public SubmittedHomeworkRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork) :
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SubmittedHomework> GetAll()
        {
            return LmsContext.SubmittedHomework.ToList();

        }

        public SubmittedHomework GetById(int Id)
        {
            return LmsContext.SubmittedHomework.Find(Id);
        }

        public bool IsSubmiited(int hwId)
        {
           var obj = LmsContext.SubmittedHomework.Where(h=>h.HomeworkID== hwId).FirstOrDefault();
            return (obj!=null) ? true : false;
        }

        string IRepository<SubmittedHomework>.Add(SubmittedHomework entity)
        {
            LmsContext.SubmittedHomework.Add(entity);
            return _unitOfWork.Commit();

        }

        int IRepository<SubmittedHomework>.Delete(int Id)
        {
            var s = LmsContext.SubmittedHomework.Find(Id);
            LmsContext.SubmittedHomework.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        int IRepository<SubmittedHomework>.update(SubmittedHomework entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }

        SubmittedHomework ISubmittedHomeworkRepo.Insert(SubmittedHomework entity)
        {
            LmsContext.SubmittedHomework.Add(entity);
            _unitOfWork.Commit();
            return entity;
        }
    }
}
