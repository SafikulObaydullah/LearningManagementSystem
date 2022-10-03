using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class DesignationVM
    {
        public int Id { get; set; }
        [DisplayName("DesgName")]
        public string DesgName { get; set; }
        [DisplayName("ShortName")]
        public string ShortName { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("DeptId")]
        public int DeptId { get; set; }
    }
}