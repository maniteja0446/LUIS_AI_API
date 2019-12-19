using LUIS_MVC5.Models;
using LUIS_MVC5.Models.Global_Classes;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LUIS_MVC5.Controllers
{
    public class LUISAppMasterController : Controller
    {
        [NonAction]
        public static IRestResponse WebAPIResponse(string jsonData, string Url, string methodtype)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest();
            switch (methodtype)
            {
                case "POST":
                    restRequest.Method = Method.POST;
                    break;
                case "GET":
                    restRequest.Method = Method.GET;
                    break;
                case "PUT":
                    restRequest.Method = Method.PUT;
                    break;
                case "DELETE":
                    restRequest.Method = Method.DELETE;
                    break;
                case "PATCH":
                    restRequest.Method = Method.PATCH;
                    break;
                default:
                    break;
            }
            restRequest.AddHeader(ConfigurationManager.AppSettings["LUIS_SubscriptionName"], ConfigurationManager.AppSettings["LUIS_SubscriptionKey"]);
            restRequest.RequestFormat = DataFormat.Json;
            var client = new RestClient(Url);
            restRequest.AddParameter("application/json; charset=utf-8", jsonData, ParameterType.RequestBody);
            restRequest.RequestFormat = DataFormat.Json;
            return client.Execute(restRequest);
        }

        // GET: LUISAppMaster
        public ActionResult Index()
        {
            LUISMasterApp lUISMasterApp = new LUISMasterApp();
            string InputJSONData = String.Empty;
            try
            {
                lUISMasterApp.Flag = ConfigurationManager.AppSettings["ReadFlag"];
                lUISMasterApp.Record_Type = ConfigurationManager.AppSettings["Record_Type"];                
                InputJSONData = JsonConvert.SerializeObject(lUISMasterApp, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.LuisAppMaster);
                List<LUISMasterApp> lUISMasterApps = new List<LUISMasterApp>();
                lUISMasterApps = JsonConvert.DeserializeObject<List<LUISMasterApp>>(output);
                if (lUISMasterApps.Count > 0)
                {

                    return View(lUISMasterApps);
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
        public static List<LUISCultures> GetCulturesList()
        {
            List<LUISCultures> lUISCultures = new List<LUISCultures>();
            try
            {
                string url = "https://westus.api.cognitive.microsoft.com/luis/api/v2.0/apps/cultures";
                IRestResponse restResponse = WebAPIResponse("", url, "GET");
                if (restResponse.IsSuccessful)
                {
                    lUISCultures = JsonConvert.DeserializeObject<List<LUISCultures>>(restResponse.Content);
                }
            }
            catch 
            {

                throw;
            }

            return lUISCultures;
        }

        public static List<LuisEndPointLocations> GetDropDownListData()
        {
            LuisEndPointLocations endPointLocations = new LuisEndPointLocations();
            List<LuisEndPointLocations> luisEndPointLocations = new List<LuisEndPointLocations>();
            string InputJSONData = "";
            try
            {
                endPointLocations.Flag = ConfigurationManager.AppSettings["ReadFlag"]; 
                endPointLocations.Record_Type = ConfigurationManager.AppSettings["Record_Type"]; 
                InputJSONData = JsonConvert.SerializeObject(endPointLocations, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.EndPointLocationCRUD);
                luisEndPointLocations = JsonConvert.DeserializeObject<List<LuisEndPointLocations>>(output);
                if (luisEndPointLocations.Count > 0)
                {

                    return luisEndPointLocations;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return luisEndPointLocations;

        }
        public static List<string> GetDomainListAPI()
        {
            List<string> domains = new List<string>();
            try
            {
                string url = "https://westus.api.cognitive.microsoft.com/luis/api/v2.0/apps/domains";
                IRestResponse restResponse = WebAPIResponse("", url, "GET");
                if (restResponse.IsSuccessful)
                {
                    domains = JsonConvert.DeserializeObject<List<string>>(restResponse.Content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return domains;
        }
        public static List<string> GetUsageSceneriosList()
        {
            List<string> lstUsageSceneriouslst = new List<string>();
            try
            {
                string url = "https://westus.api.cognitive.microsoft.com/luis/api/v2.0/apps/usagescenarios";
                IRestResponse restResponse = WebAPIResponse("", url, "GET");
                if (restResponse.IsSuccessful)
                {
                    lstUsageSceneriouslst = JsonConvert.DeserializeObject<List<string>>(restResponse.Content);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return lstUsageSceneriouslst;
        }
        [HttpPost]
        public JsonResult GetTokenNumberBasedonCulture(string code)
        {
            TokenizerVersions tokens = new TokenizerVersions();
            try
            {
                string url = "https://westus.api.cognitive.microsoft.com/luis/api/v2.0/apps/cultures/"+code+"/tokenizerversions";
                IRestResponse restResponse = WebAPIResponse("", url, "GET");
                if (restResponse.IsSuccessful)
                {
                    tokens = JsonConvert.DeserializeObject<TokenizerVersions>(restResponse.Content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<string> mani = new List<string>();
            mani = tokens.tokenizerVersions;
            return Json(new SelectList(mani));
        }

        // GET: LUISAppMaster/Details/5
        public ActionResult Details(int id)
        {
            LUISMasterApp lUISMasterApp = new LUISMasterApp();
            string InputJSONData = String.Empty;
            try
            {
                lUISMasterApp.Flag = ConfigurationManager.AppSettings["ReadFlag"]; 
                lUISMasterApp.Record_Type = ConfigurationManager.AppSettings["Record_Type"]; 
                lUISMasterApp.AID = id.ToString();
                InputJSONData = JsonConvert.SerializeObject(lUISMasterApp, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.LuisAppMaster);
                List<LUISMasterApp> lUISMasterApps = new List<LUISMasterApp>();
                lUISMasterApps = JsonConvert.DeserializeObject<List<LUISMasterApp>>(output);
                if (lUISMasterApps.Count > 0)
                {
                    lUISMasterApp = lUISMasterApps[0];
                    return View(lUISMasterApp);
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
            //return View();
        }

        // GET: LUISAppMaster/Create
        public ActionResult Create()
        {
            LUISMasterApp lUISMasterApp = new LUISMasterApp()
            {
                LuisEndPointLocations = GetDropDownListData(),
                UsageScenarioList = GetUsageSceneriosList(),
                LUISCulturesList = GetCulturesList(),
                GetDomainsList = GetDomainListAPI()
            };
            return View(lUISMasterApp);
        }

        // POST: LUISAppMaster/Create
        [HttpPost]
        public ActionResult Create(LUISMasterApp lUISMasterApp)
        {
            string InputJSONData = String.Empty;
            List<ErrorMessageHandler> errorMessageHandlers = new List<ErrorMessageHandler>();
            try
            {
                var EndPointobj = GetDropDownListData().FirstOrDefault(x => x.LID == lUISMasterApp.EndPointID);
                var luisinputdata = new { name = lUISMasterApp.AName, description = lUISMasterApp.ADescription, culture = lUISMasterApp.ACulture, tokenizerVersion = lUISMasterApp.AtokenizerVersion, usageScenario = lUISMasterApp.AUsageScenario, domain = lUISMasterApp.Adomain, initialVersionId = lUISMasterApp.AinitialVersionId };
                InputJSONData = JsonConvert.SerializeObject(luisinputdata, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string MyUrl = "https://" + EndPointobj.LEndPointCode + ".api.cognitive.microsoft.com/luis/api/v2.0/apps/";
                IRestResponse restResponse = WebAPIResponse(InputJSONData, MyUrl, "POST");
                if (restResponse.IsSuccessful)                
                {
                    lUISMasterApp.Record_Type = ConfigurationManager.AppSettings["Record_Type"]; 
                    lUISMasterApp.Flag = ConfigurationManager.AppSettings["InsertFlag"];
                    lUISMasterApp.InsertedDate = DateTime.Now.ToString();
                    lUISMasterApp.AppID = JsonConvert.DeserializeObject<string>(restResponse.Content);                    
                    lUISMasterApp.EndPointID = EndPointobj.LID;
                    InputJSONData = JsonConvert.SerializeObject(lUISMasterApp, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.LuisAppMaster);
                    errorMessageHandlers = JsonConvert.DeserializeObject<List<ErrorMessageHandler>>(output);
                    if (errorMessageHandlers == null || errorMessageHandlers.Count == 0 || errorMessageHandlers[0].ErrorNumber != "0")
                    {
                        string message = "Please check your Database Procedure with Above JSON Data";
                        var returnexception = new Exception(message, restResponse.ErrorException);
                        throw returnexception;
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else if (restResponse.ErrorException != null)
                {
                    string message = "Please check your API with InputJSON data in POST MAN";
                    var returnexception = new Exception(message, restResponse.ErrorException);
                    throw returnexception;
                }
            }
            catch
            {
                return RedirectToAction("Error");
            }
            if (errorMessageHandlers.Count > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        // GET: LUISAppMaster/Edit/5
        public ActionResult Edit(int id)
        {            
            LUISMasterApp lUISMasterApp = new LUISMasterApp();
            string InputJSONData = String.Empty;
            try
            {
                lUISMasterApp.Flag = ConfigurationManager.AppSettings["ReadFlag"];
                lUISMasterApp.Record_Type = ConfigurationManager.AppSettings["Record_Type"];
                lUISMasterApp.AID = id.ToString();
                InputJSONData = JsonConvert.SerializeObject(lUISMasterApp, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.LuisAppMaster);
                List<LUISMasterApp> lUISMasterApps = new List<LUISMasterApp>();
                lUISMasterApps = JsonConvert.DeserializeObject<List<LUISMasterApp>>(output);
                if (lUISMasterApps.Count > 0)
                {
                    lUISMasterApp = lUISMasterApps[0];
                    lUISMasterApp.LUISCulturesList = GetCulturesList();
                    lUISMasterApp.UsageScenarioList = GetUsageSceneriosList();
                    lUISMasterApp.GetDomainsList = GetDomainListAPI();
                    lUISMasterApp.LuisEndPointLocations = GetDropDownListData();
                    return View(lUISMasterApp);
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

        // POST: LUISAppMaster/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, LUISMasterApp lUISMasterApp)
        {
            string InputJSONData = String.Empty;
            List<ErrorMessageHandler> errorMessageHandlers = new List<ErrorMessageHandler>();
            try
            {
                var EndPointobj = GetDropDownListData().FirstOrDefault(x => x.LID == lUISMasterApp.EndPointID);
                var luisinputdata = new { name = lUISMasterApp.AName, description = lUISMasterApp.ADescription };
                InputJSONData = JsonConvert.SerializeObject(luisinputdata, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string MyUrl = "https://" + EndPointobj.LEndPointCode + ".api.cognitive.microsoft.com/luis/api/v2.0/apps/"+lUISMasterApp.AppID;
                IRestResponse restResponse = WebAPIResponse(InputJSONData, MyUrl, "PUT");
                if (restResponse.IsSuccessful)                
                {
                    lUISMasterApp.Record_Type = ConfigurationManager.AppSettings["Record_Type"]; 
                    lUISMasterApp.Flag = ConfigurationManager.AppSettings["UpdateFlag"]; 
                    lUISMasterApp.UpdatedDate = DateTime.Now.ToString();                    
                    InputJSONData = JsonConvert.SerializeObject(lUISMasterApp, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.LuisAppMaster);
                    errorMessageHandlers = JsonConvert.DeserializeObject<List<ErrorMessageHandler>>(output);
                    if (errorMessageHandlers == null || errorMessageHandlers.Count == 0 || errorMessageHandlers[0].ErrorNumber != "0")
                    {
                        string message = "Please check your Database Procedure with Above JSON Data";
                        var returnexception = new Exception(message, restResponse.ErrorException);                        
                        throw returnexception;
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else if (restResponse.ErrorException != null)
                {
                    string message = "Please check your API with data in POST MAN";
                    var returnexception = new Exception(message, restResponse.ErrorException);
                    throw returnexception;
                }
            }
            catch
            {
                return View("Error");
            }
            if (errorMessageHandlers.Count > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }
        }

        // GET: LUISAppMaster/Delete/5
        public ActionResult Delete(int id)
        {
            LUISMasterApp lUISMasterApp = new LUISMasterApp();
            string InputJSONData = String.Empty;
            try
            {
                lUISMasterApp.Flag = ConfigurationManager.AppSettings["ReadFlag"];
                lUISMasterApp.Record_Type = ConfigurationManager.AppSettings["Record_Type"];
                lUISMasterApp.AID = id.ToString();
                InputJSONData = JsonConvert.SerializeObject(lUISMasterApp, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.LuisAppMaster);
                List<LUISMasterApp> lUISMasterApps = new List<LUISMasterApp>();
                lUISMasterApps = JsonConvert.DeserializeObject<List<LUISMasterApp>>(output);
                if (lUISMasterApps.Count > 0)
                {
                    lUISMasterApp = lUISMasterApps[0];
                    lUISMasterApp.LUISCulturesList = GetCulturesList();
                    lUISMasterApp.UsageScenarioList = GetUsageSceneriosList();
                    lUISMasterApp.GetDomainsList = GetDomainListAPI();
                    lUISMasterApp.LuisEndPointLocations = GetDropDownListData();
                    return View(lUISMasterApp);
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

        // POST: LUISAppMaster/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, LUISMasterApp lUISMasterApp)
        {
            string InputJSONData = String.Empty;
            List<ErrorMessageHandler> errorMessageHandlers = new List<ErrorMessageHandler>();
            try
            {
                var EndPointobj = GetDropDownListData().FirstOrDefault(x => x.LID == lUISMasterApp.EndPointID);
                string url = "https://"+ EndPointobj.LEndPointCode + ".api.cognitive.microsoft.com/luis/api/v2.0/apps/"+lUISMasterApp.AppID;
                IRestResponse restResponse = WebAPIResponse("", url, "DELETE");
                if (restResponse.IsSuccessful)
                {
                    lUISMasterApp.Record_Type = ConfigurationManager.AppSettings["DeleteFlag"];
                    lUISMasterApp.Flag = ConfigurationManager.AppSettings["DeleteFlag"];
                    lUISMasterApp.DeletedDate = DateTime.Now.ToString();
                    lUISMasterApp.AID = id.ToString();
                    InputJSONData = JsonConvert.SerializeObject(lUISMasterApp, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    string output = DBHelper.ExecuteDatabaseOperations(InputJSONData, Global_StoredProcedures.LuisAppMaster);
                    errorMessageHandlers = JsonConvert.DeserializeObject<List<ErrorMessageHandler>>(output);
                    if (errorMessageHandlers == null || errorMessageHandlers.Count == 0 || errorMessageHandlers[0].ErrorNumber != "0")
                    {
                        string message = "Please check your Database Procedure with Above JSON Data";
                        var returnexception = new Exception(message, restResponse.ErrorException);
                        throw returnexception;
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Error");
                }               
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
