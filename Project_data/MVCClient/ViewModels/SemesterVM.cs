using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class SemesterVM
    {
        public int Id { get; set; }
        [DisplayName("SemesterName")]
        public string SemesterName { get; set; }
        [DisplayName("ShortName")]
        public string ShortName { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}