using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LUIS_MVC5.Models;
using LUIS_MVC5.Models.Global_Classes;
using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
namespace LUIS_MVC5.Controllers
{
    public class EndPointLocationsController : Controller
    {
        // GET: EndPointLocations
        public ActionResult Index()
        {
            LuisEndPointLocations endPointLocations = new LuisEndPointLocations();
            string InputJSONData = String.Empty;
            try
            {
                endPointLocations.Flag = ConfigurationManager.AppSettings["ReadFlag"];
                endPointLocations.Record_Type = ConfigurationManager.AppSettings["Record_Type"];
                endPointLocations.InsertedDate = DateTime.Now.ToString();
                
                InputJSONData = JsonConvert.SerializeObject(endPointLocations, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.EndPointLocationCRUD);
                List<LuisEndPointLocations> luisEndPointLocations = new List<LuisEndPointLocations>();
                luisEndPointLocations = JsonConvert.DeserializeObject<List<LuisEndPointLocations>>(output);                
                if (luisEndPointLocations.Count > 0)
                {
                    return View(luisEndPointLocations);
                }
                else
                {
                    return RedirectToAction("Mani");
                }
            }
            catch
            {
                return View();
            }            
        }        
        public ActionResult Details(int id)
        {
            LuisEndPointLocations luisEndPointLocations = new LuisEndPointLocations();
            string InputJSONData = "";
            try
            {
                luisEndPointLocations.Flag = ConfigurationManager.AppSettings["ReadFlag"];
                luisEndPointLocations.Record_Type = ConfigurationManager.AppSettings["Record_Type"];
                luisEndPointLocations.LID = id;
                InputJSONData = JsonConvert.SerializeObject(luisEndPointLocations, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.EndPointLocationCRUD);
                List<LuisEndPointLocations> luisEndPointLocationslist = new List<LuisEndPointLocations>();
                luisEndPointLocationslist = JsonConvert.DeserializeObject<List<LuisEndPointLocations>>(output);
                if (luisEndPointLocationslist.Count > 0)
                {
                    luisEndPointLocations = luisEndPointLocationslist[0];
                    return View(luisEndPointLocations);
                }
                else
                {
                    return RedirectToAction("Mani");
                }
            }
            catch 
            {
                return View("Error");
            }
            
        }
        // GET: EndPointLocations/Create
        public ActionResult Create()
        {            
            return View();
        }
        // POST: EndPointLocations/Create
        [HttpPost]
        public ActionResult Create(LuisEndPointLocations endPointLocations)
        {
            string InputJSONData = String.Empty;
            try
            {
                endPointLocations.Flag = ConfigurationManager.AppSettings["InsertFlag"];
                endPointLocations.Record_Type = ConfigurationManager.AppSettings["Record_Type"];
                endPointLocations.InsertedDate = DateTime.Now.ToString();               
                InputJSONData = JsonConvert.SerializeObject(endPointLocations,Formatting.Indented,new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore});
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData,Global_StoredProcedures.EndPointLocationCRUD);
                List<ErrorMessageHandler> errorMessageHandlers = new List<ErrorMessageHandler>();
                errorMessageHandlers = JsonConvert.DeserializeObject<List<ErrorMessageHandler>>(output);
                if (errorMessageHandlers.Count > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Mani");
                }                
            }
            catch
            {
                return View();
            }
        }
        // GET: EndPointLocations/Edit/5
        public ActionResult Edit(int id)
        {
            LuisEndPointLocations luisEndPointLocations = new LuisEndPointLocations();
            string InputJSONData = String.Empty;
            try
            {
                luisEndPointLocations.Flag = ConfigurationManager.AppSettings["ReadFlag"]; 
                luisEndPointLocations.Record_Type = ConfigurationManager.AppSettings["Record_Type"]; 
                luisEndPointLocations.LID = id;
                InputJSONData = JsonConvert.SerializeObject(luisEndPointLocations, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.EndPointLocationCRUD);
                List<LuisEndPointLocations> luisEndPointLocationslist = new List<LuisEndPointLocations>();
                luisEndPointLocationslist = JsonConvert.DeserializeObject<List<LuisEndPointLocations>>(output);
                if (luisEndPointLocationslist.Count > 0)
                {
                    luisEndPointLocations = luisEndPointLocationslist[0];
                    return View(luisEndPointLocations);
                }
                else
                {
                    return RedirectToAction("Mani");
                }
            }
            catch 
            {
                return View("Error");
            }
            
        }
        // POST: EndPointLocations/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, LuisEndPointLocations luisEndPointLocations)
        {
            string InputJSONData = String.Empty;
            try
            {
                luisEndPointLocations.Flag = ConfigurationManager.AppSettings["UpdateFlag"];
                luisEndPointLocations.Record_Type = ConfigurationManager.AppSettings["Record_Type"];
                luisEndPointLocations.InsertedDate = DateTime.Now.ToString();
                luisEndPointLocations.LID = id;
                InputJSONData = JsonConvert.SerializeObject(luisEndPointLocations, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.EndPointLocationCRUD);
                List<ErrorMessageHandler> errorMessageHandlers = new List<ErrorMessageHandler>();
                errorMessageHandlers = JsonConvert.DeserializeObject<List<ErrorMessageHandler>>(output);
                if (errorMessageHandlers.Count > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }
            catch 
            {
                return View();
            }
        }
        // GET: EndPointLocations/Delete/5
        public ActionResult Delete(int id)
        {
            LuisEndPointLocations luisEndPointLocations = new LuisEndPointLocations();
            string InputJSONData = String.Empty;
            try
            {
                luisEndPointLocations.Flag = ConfigurationManager.AppSettings["ReadFlag"]; 
                luisEndPointLocations.Record_Type = ConfigurationManager.AppSettings["Record_Type"]; 
                luisEndPointLocations.LID = id;
                InputJSONData = JsonConvert.SerializeObject(luisEndPointLocations, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.EndPointLocationCRUD);
                List<LuisEndPointLocations> luisEndPointLocationslist = new List<LuisEndPointLocations>();
                luisEndPointLocationslist = JsonConvert.DeserializeObject<List<LuisEndPointLocations>>(output);
                if (luisEndPointLocationslist.Count > 0)
                {
                    luisEndPointLocations = luisEndPointLocationslist[0];
                    return View(luisEndPointLocations);
                }
                else
                {
                    return RedirectToAction("Mani");
                }
            }
            catch 
            {
                return View("Error");
            }
            
        }
        // POST: EndPointLocations/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, LuisEndPointLocations luisEndPointLocations)
        {
            string InputJSONData = String.Empty;
            List<ErrorMessageHandler> errorMessageHandlers = new List<ErrorMessageHandler>();
            try
            {
                    luisEndPointLocations.Record_Type = ConfigurationManager.AppSettings["DeleteFlag"];
                    luisEndPointLocations.Flag = ConfigurationManager.AppSettings["DeleteFlag"];
                    luisEndPointLocations.DeletedDate = DateTime.Now.ToString();
                    luisEndPointLocations.LID = id;
                    InputJSONData = JsonConvert.SerializeObject(luisEndPointLocations, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.EndPointLocationCRUD);
                    errorMessageHandlers = JsonConvert.DeserializeObject<List<ErrorMessageHandler>>(output);
                    if (errorMessageHandlers == null || errorMessageHandlers.Count == 0 || errorMessageHandlers[0].ErrorNumber != "0")
                    {
                        string message = "Please check your Database Procedure with Above JSON Data";
                        var returnexception = new Exception(message);
                        throw returnexception;
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }             
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
