using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;

namespace LUIS_MVC5.Models
{
    public static class DBHelper
    {
        static string Constr = ConfigurationManager.ConnectionStrings["Maniteja"].ConnectionString;
        public static string ExecuteDatabaseOperations(string InputJSONData,string ProcedureName)
        {
            SqlConnection sqlConnection;
            SqlCommand sqlCommand;
            DataTable dataTable;
            SqlDataAdapter sqlDataAdapter;
            string JSONData = "" ;
            try
            {
                sqlConnection = new SqlConnection(Constr);
                dataTable = new DataTable();
                using (sqlConnection)
                {
                    sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = ProcedureName;
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.CommandTimeout = 0;
                    sqlCommand.Parameters.AddWithValue("@json", InputJSONData);
                    sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(dataTable);
                }
                JSONData = dataTable.Rows[0][0].ToString();                
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            finally
            {
                
            }
            return JSONData;        
        }
    }
}