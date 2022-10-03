using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class CommentVM
    {
        public int Id { get; set; }
        [ForeignKey("Homework")]
        public int HomeWorkId { get; set; }
        public string Comments { get; set; }
        [ForeignKey("Student")]
        public int StdId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CommentsDate { get; set; }
        public StudentVM Student { get; set; }
        public HomeworkVM HomeworkVM { get; set; }
    }
}