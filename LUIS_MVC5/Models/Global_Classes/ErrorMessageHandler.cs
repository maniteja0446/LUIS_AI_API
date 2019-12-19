using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LUIS_MVC5.Models.Global_Classes
{
    public class ErrorMessageHandler
    {
        public string ErrorNumber { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorSeverity { get; set; }
        public string ErrorState { get; set; }
        public string ErrorProcedure { get; set; }
        public string ErrorLine { get; set; }
    }
}