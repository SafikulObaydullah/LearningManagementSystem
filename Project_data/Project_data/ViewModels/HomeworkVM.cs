using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_data.ViewModels
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

        public string Teacher { get; set; }
        public int SubId { get; set; }
        public string SubjectName { get; set; }
        public int ClassId { get; set; }
        public string ClassNames { get; set; }
        public int SecID { get; set; }
        public string SectionName { get; set; }
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string Status { get; set; }

    }
}
