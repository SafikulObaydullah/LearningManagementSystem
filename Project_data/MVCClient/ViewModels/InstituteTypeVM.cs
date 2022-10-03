using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class InstituteTypeVM
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<InstituteInfoVM> InstituteInfosVM { get; set; }
    }
}