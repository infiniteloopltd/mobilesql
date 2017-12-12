using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MobileSQL
{
    public partial class api : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                var strDataSource = Request.QueryString["DataSource"];
                var strUsername = Request.QueryString["Username"];
                var strPassword = Request.QueryString["Password"];
                var strSQL = Request.QueryString["SQL"];
                var strDB = Request.QueryString["DB"].ToLower();
                var dt = new DataTable();
                if (strDB == "sql")
                {
                    dt = ExecuteMSSQL(strDataSource, strUsername, strPassword, strSQL);
                }
                else
                {
                    dt = ExecuteMYSQL(strDataSource, strUsername, strPassword, strSQL);
                }
                string result = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);
                Response.Write(result);
            }
            catch(Exception ex)
            {
                string result = JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented);
                Response.Write(result);
            }
        }


        private DataTable ExecuteMSSQL(string strDataSource, string strUsername, string strPassword, string strSQL)
        {
            var strDSNTemplate = "Network Library=DBMSSOCN;Data Source={0}; User ID={1}; Password={2};";
            var sqlConnection = string.Format(strDSNTemplate, strDataSource, strUsername, strPassword);
            var adapter = new SqlDataAdapter(strSQL, sqlConnection);
            var dataSet = new DataSet();
            adapter.Fill(dataSet, "sql");
            var dt = dataSet.Tables["sql"];
            return dt;
        }


        private DataTable ExecuteMYSQL(string strDataSource, string strUsername, string strPassword, string strSQL)
        {
            var strDSNTemplate = "server={0}; uid={1}; pwd={2};";
            var sqlConnection = string.Format(strDSNTemplate, strDataSource, strUsername, strPassword);
            var adapter = new MySqlDataAdapter(strSQL, sqlConnection);
            var dataSet = new DataSet();
            adapter.Fill(dataSet, "sql");
            var dt = dataSet.Tables["sql"];
            return dt;
        }
    }
}