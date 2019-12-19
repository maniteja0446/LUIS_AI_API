using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace LUIS_MVC5.Models
{
    /// <summary>
    /// We are creating endpoint location class for single time usable and if any new enpoints are involved in future, we can related records by using this master page 
    /// We're inheriting global class to ledgering the data between User & Admin
    /// </summary>
    public class LuisEndPointLocations : Global_Classes.Global_Records
    {
        [DisplayName("LID")]
        public int LID { get; set; }
        
        [Required(ErrorMessage ="Please Enter Location Name")]
        [DisplayName("End Point Location Name")]
        public string LEndPoint { get; set; }
        [DisplayName("End Point Default Url")]
        public string LEndPointUrl { get; set; }
        [DisplayName("End Point Location Code")]
        public string LEndPointCode { get; set; }
        
    }
}