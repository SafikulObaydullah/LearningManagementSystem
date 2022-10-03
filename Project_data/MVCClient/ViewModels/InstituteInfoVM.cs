using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class InstituteInfoVM
    {
        public int Id { get; set; }
        public string InstituteName { get; set; }

        public string Logo { get; set; }
        [ForeignKey("InstituteType")]
        public int TypeID { get; set; }
        public InstituteTypeVM InstituteTypeVM { get; set; }
    }
}