using Project_data;
using Project_data.Helper;
using Project_data.Infrastructure.Concrete;
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
    public class StudentsController : ApiController
    {
        RepositoryAccess repositoryAccess = new RepositoryAccess();
        public StudentsController()
        {

        }
      public string Post()
      {
         string fPath = "";
         string apiPath = "";
         var modelsMessage = new ModelsMessage();
         var httpRequest = HttpContext.Current.Request;
         //if (httpRequest.Files.Count < 1)
         //{
         //   modelsMessage.Message = HttpStatusCode.BadRequest.ToString();
         //   return modelsMessage;
         //}
         fPath = Path.Combine(HttpContext.Current.Server.MapPath("~/"), "Images/StdPic");
         if (!Directory.Exists(fPath))
         {
            Directory.CreateDirectory(fPath);
         }
         string ext = Path.GetExtension(httpRequest.Files[0].FileName);

         var name = httpRequest.Params["Name"];
         string fname = name + ext;
         HttpPostedFile file = httpRequest.Files[0];
         file.SaveAs(Path.Combine(fPath, fname));
         apiPath = "Images/StdPic/" + fname;

         var gender = httpRequest.Params["Gender"];
         //var fName = httpRequest.Params["StudentFatherName"];
         //var fContract = httpRequest.Params["StudentFathersContract"];
         //var mName = httpRequest.Params["StudentMotherName"];
         //var mContact = httpRequest.Params["StudentMothersContract"];
         var sContract = httpRequest.Params["ContactNo"];
         var email = httpRequest.Params["Email"];
         var dob = httpRequest.Params["DOB"];
         //var birthNo = httpRequest.Params[" "];
         var InstId = httpRequest.Params["InstId"];
         Gender? g = null;
         if (Enum.IsDefined(typeof(Gender), gender))
         {
            // This will parse the string into it's corresponding gender enum object.
            g = (Gender)Enum.Parse(typeof(Gender), gender);
         }
         var studentBasicInfo = new Student
         {
            //ReligionId = int.Parse(religion),
            //StudentGender = gender,
            Gender = (Gender)g,
            //StudentBirthRegNo = birthNo,
            Name = name,
            Email = email,
            //StudentFatherName = fName,
            //StudentFathersContract = fContract,
            ContactNo = sContract,
            //StudentMotherName = mName,
            //StudentMothersContract = mContact,
            DOB = DateTime.Parse(dob),
            ImagePath = apiPath,
            InstId = int.Parse(InstId)
         };
         return repositoryAccess.StudentRepo.Add(studentBasicInfo);
         //return repositoryAccess.UnitOfWork.Commit();

      }
      //public string Post(Student student)
      //{
      //   repositoryAccess.StudentRepo.Add(student);
      //   return repositoryAccess.UnitOfWork.Commit();
      //}
      public string Delete(int id)
        {
            repositoryAccess.StudentRepo.Delete(id);
            return repositoryAccess.UnitOfWork.Commit();
        }
        public IEnumerable<Student> Get()
        {
            return repositoryAccess.StudentRepo.GetAll();
        }
        public Student Get(int id)
        {
            return repositoryAccess.StudentRepo.GetById(id);
        }
        public string Put(Student student)
        {
            repositoryAccess.StudentRepo.update(student);
            return repositoryAccess.UnitOfWork.Commit();
        }
    }
}
