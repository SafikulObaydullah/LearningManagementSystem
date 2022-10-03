using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class StudentVM
    {
      public int Id { get; set; }
      [DataType(DataType.Date)]
      [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
      [DisplayName("DOB")]
      public DateTime DOB { get; set; }
      [DisplayName("Name")]
      public string Name { get; set; }
      [DisplayName("ContactNo")]
      public string ContactNo { get; set; }
      [DisplayName("Gender")]
      public GenderVM Gender { get; set; }
      [DisplayName("Email")]
      public string Email { get; set; }
      [DisplayName("PresentAdd")]
      public string PresentAdd { get; set; }
      [DisplayName("PermanentAdd")]
      public string PermanentAdd { get; set; }
      [DisplayName("ImagePath")]
      public string ImagePath { get; set; }
      public HttpPostedFileBase StudentPicture { get; set; }
      [DisplayName("Select Insttitute")]
      public int InstId { get; set; }
      [DisplayName("submittedHomeworksVM")]
      public ICollection<SubmittedHomeworkVM> submittedHomeworksVM { get; set; }
   }
}