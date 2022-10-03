using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class SubJectVM
    {
        public int Id { get; set; }
        [DisplayName("Subject")]
        public string SubName { get; set; }
        [DisplayName("Short Name")]
        public string ShortName { get; set; }
        public string Description { get; set; }
        [DisplayName("Progarm Name")]
        public int GroupProgramId { get; set; }
        public string ImagePath { get; set; }
    }
}