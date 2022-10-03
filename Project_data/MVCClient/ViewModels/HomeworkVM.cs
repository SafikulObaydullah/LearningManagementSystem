using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class HomeworkVM
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        [MaxLength]
        public string Description { get; set; }

        public int TeacherID { get; set; }
        [Display(Name ="Teacher Name")]
        public string Teacher { get; set; }
        [Display(Name = "Select Subject Name")]
        public int SubId { get; set; }
        public string SubjectName { get; set; }
        [Display(Name = "Select Class Name")]
        public int ClassId { get; set; }
        public string ClassNames { get; set; }
        [Display(Name = "Select Section Name")]
        public int SecID { get; set; }
        public string SectionName { get; set; }
        [Display(Name = "Select Shift Name")]
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public HomeworkStatus Status { get; set; } = HomeworkStatus.NotSubmitted;
        public bool IsSubmitted { get; set; }
        public string SubjectImage { get; set; }
        //public EmployeeVM Employee { get; set; }
        //public SubJectVM Subject { get; set; }
        //public ClassNameVM ClassName { get; set; }
        //public SectionVM Section { get; set; }
        //public ShiftVM Shift { get; set; }
    }
    public enum HomeworkStatus
    {
        Submitted = 1,
        NotSubmitted = 2
    }

}