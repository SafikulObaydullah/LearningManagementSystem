
using Project_data;
using Project_data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Project_API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class CommentsController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public CommentsController()
        {

        }
        public string Post(Comment comment)
        {
            repositoryAccess.CommentRepo.Add(comment);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public string Delete(int id)
        {
            repositoryAccess.CommentRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<Comment> Get()
        {
           return repositoryAccess.CommentRepo.GetAll();
        }
        public Comment Get(int id)
        {
            return repositoryAccess.CommentRepo.GetById(id);
        }
        public string Put(Comment comment)
        {
            repositoryAccess.CommentRepo.update(comment);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
