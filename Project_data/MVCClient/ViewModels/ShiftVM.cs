using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class ShiftVM
    {
        public int Id { get; set; }
        [DisplayName("SiftName")]
        public string SiftName { get; set; }
    }
}