using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        [DisplayName("DOB")]
        public DateTime DOB { get; set; }
        [DisplayName("JoiningDate")]
        public DateTime JoiningDate { get; set; }
        [StringLength(50)]
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("ContactNo")]
        public string ContactNo { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("PresentAdd")]
        public string PresentAdd { get; set; }
        [DisplayName("PermanentAdd")]
        public string PermanentAdd { get; set; }
        [DisplayName("DesId")]
        public int DesId { get; set; }
        [DisplayName("DeptId")]
        public int DeptId { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        [DisplayName("TypeID")]
        public int TypeID { get; set; }
        [DisplayName("HomeWorks")]
        public ICollection<HomeworkVM> HomeWorks { get; set; }
        [DisplayName("EmployeeType")]
        public EmployeeTypeVM EmployeeType { get; set; }
    }
}