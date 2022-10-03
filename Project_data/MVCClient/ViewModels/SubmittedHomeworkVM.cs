using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class SubmittedHomeworkVM
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmittedDate { get; set; }
        [StringLength(200)]
        [DisplayName("Title")]
        public string Title { get; set; }
        [MaxLength]
        [DisplayName("Description")]
        public string Description { get; set; }
        [ForeignKey("Homework")]
        [DisplayName("HomeworkID")]
        public int HomeworkID { get; set; }
        [ForeignKey("Student")]
        [DisplayName("StudentID")]
        public int StudentID { get; set; }
        public HttpPostedFileBase HomeworkFiles { get; set; }
        [DisplayName("Status")]
        public string Status { get; set; }
        public HomeworkVM Homework { get; set; }
        public StudentVM studentVM { get; set; }
    }
}