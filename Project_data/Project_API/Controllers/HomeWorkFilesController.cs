using Project_data;
using Project_data.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Project_API.Controllers
{
    [EnableCors("*", "*", "*")]
    public class HomeWorkFilesController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public HomeWorkFilesController()
        {

        }
        public string Post(HomeWorkFile homeWorkFile)
        {
            HomeWorkFile hf = new HomeWorkFile();
            string msg = "";
            var context = HttpContext.Current.Request;

            var h = Request.Content.Headers;
            if (context.Files != null)
            {
                foreach (string file in context.Files)
                {
                    var postedFiles = context.Files[file];
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/Homework/")))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Homework/"));
                    }
                    string fPath = HttpContext.Current.Server.MapPath("~/Homework/" + postedFiles.FileName);
                    postedFiles.SaveAs(fPath);
                    hf.SubmittedHomeworkId = int.Parse(context.Params["SubmittedHomeworkId"]);
                    hf.AnswerPath = "~/Homework/" + postedFiles.FileName;
                    hf.SubmittedHomeworkId = int.Parse(context.Params["SubmittedHomeworkId"]);
                    repositoryAccess.HomeWorkFileRepo.Add(hf);
                    msg = repositoryAccess.UnitOfWork.Commit();
                }
            }

            return msg;
        }
        public string Delete(int id)
        {
            repositoryAccess.HomeWorkFileRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<HomeWorkFile> Get()
        {
            return repositoryAccess.HomeWorkFileRepo.GetAll();
        }
        public HomeWorkFile Get(int id)
        {
            return repositoryAccess.HomeWorkFileRepo.GetById(id);
        }
        public string Put(HomeWorkFile homeWorkFile)
        {
            repositoryAccess.HomeWorkFileRepo.update(homeWorkFile);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
