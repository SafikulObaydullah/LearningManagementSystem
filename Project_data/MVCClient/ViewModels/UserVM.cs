using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCClient.ViewModels
{
    public class UserVM
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RolesName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}