using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class HomeWorkFileVM
    {
        public int Id { get; set; }
        [ForeignKey("SubmittedHomeworkVM")]
        [DisplayName("SubmittedHomeworkId")]
        public int SubmittedHomeworkId { get; set; }
        [DisplayName("AnswerPath")]
        public string AnswerPath { get; set; }
        [DisplayName("submittedHomeworkVM")]
        public SubmittedHomeworkVM submittedHomeworkVM { get; set; }
    }
}