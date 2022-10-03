using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class GroupProgramVM
    {
        public int Id { get; set; }
        [DisplayName("GroupProgramName")]
        public string GroupProgramName { get; set; }
        [DisplayName("ShortName")]
        public string ShortName { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [ForeignKey("InstituteType")]
        public int InstitteTypeID { get; set; }
        public InstituteTypeVM InstituteType { get; set; }
    }
}