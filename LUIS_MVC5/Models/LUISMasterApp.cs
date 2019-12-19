using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LUIS_MVC5.Models
{
    public class LUISMasterApp:Global_Classes.Global_Records
    {
        
        [DisplayName("LUIS APP ID")]
        public string AID { get; set; }

        [Required(ErrorMessage = "Please Enter App Name")]
        [DisplayName("App Name")]
        public string AName { get; set; }
        [Required(ErrorMessage = "Please Enter Description")]
        [DisplayName("Description")]
        public string ADescription { get; set; }
        [Required(ErrorMessage = "Please Enter Culture Name")]
        [DisplayName("Culture Name")]
        public string ACulture { get; set; }
        [Required(ErrorMessage = "Please Enter Token Version")]
        [DisplayName("Token Version")]
        public string AtokenizerVersion { get; set; }
        [Required(ErrorMessage = "Please Enter Usage Scenario")]
        [DisplayName("Usage Scenario")]
        public string AUsageScenario { get; set; }
        [Required(ErrorMessage = "Please Enter Domain")]
        [DisplayName("Domain Name")]
        public string Adomain { get; set; }
        [Required(ErrorMessage = "Please Enter Initial Version ID")]
        [DisplayName("Initial Version ID")]
        public string AinitialVersionId { get; set; }
        [Required(ErrorMessage = "Please Enter End Point")]
        [DisplayName("EndPoint ID")]
        public int EndPointID { get; set; }
        
        [DisplayName("Return ID")]
        public string AppID { get; set; }       

        public List<LuisEndPointLocations> LuisEndPointLocations { get; set; }
        public List<string> UsageScenarioList { get; set; }
        public List<LUISCultures> LUISCulturesList { get; set; }
        public List<string> GetDomainsList { get; set; }

    }
}