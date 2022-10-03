using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCClient.EmailModel
{
   public class MailMessage
   {
      public List<string> To { get; set; }
      public List<string> Cc { get; set; }
      public List<string> Bcc { get; set; }
      public string Subject { get; set; }
      public string Body { get; set; }
   }
}