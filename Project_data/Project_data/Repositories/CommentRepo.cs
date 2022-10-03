using Project_data.Infrastructure.Concrete;
using Project_data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.Repositories
{
    public interface ICommentRepo : IRepository<Comment>
    {

    }
    public class CommentRepo : BasicRepository<Comment>, ICommentRepo
    {
        IUnitOfWork _unitOfWork;
        public CommentRepo(IdatabaseFactory idatabaseFactory,IUnitOfWork unitOfWork):
            base(idatabaseFactory)
        {
            _unitOfWork = unitOfWork;
        }
        string IRepository<Comment>.Add(Comment entity)
        {
            LmsContext.Comments.Add(entity);
            return _unitOfWork.Commit();
        }

        int IRepository<Comment>.Delete(int Id)
        {
            var s = LmsContext.Comments.Find(Id);
            LmsContext.Comments.Remove(s);
            LmsContext.Commit();
            return 1;
        }

        public IEnumerable<Comment> GetAll()
        {
            return LmsContext.Comments.ToList();
        }

        public Comment GetById(int Id)
        {
            return LmsContext.Comments.Find(Id);
        }

        int IRepository<Comment>.update(Comment entity)
        {
            LmsContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _unitOfWork.Commit();
            return 1;
        }
        
    }
}
