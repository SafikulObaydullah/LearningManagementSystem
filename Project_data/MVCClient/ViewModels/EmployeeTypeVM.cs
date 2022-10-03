using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class EmployeeTypeVM
    {
        public int Id { get; set; }
        [DisplayName("TypeName")]
        public string TypeName { get; set; }
        [DisplayName("employeesVM")]
        public ICollection<EmployeeVM> employeesVM { get; set; }
    }
}