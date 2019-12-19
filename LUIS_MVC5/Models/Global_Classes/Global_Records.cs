using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LUIS_MVC5.Models.Global_Classes
{
    public class Global_Records
    {
        
        public string Flag { get; set; }
        public string Record_Type { get; set; }
        public string InsertedDate { get; set; }
        public string DeletedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string IPAddress { get => GetIPAddress(); set => GetIPAddress(); }
        private string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}