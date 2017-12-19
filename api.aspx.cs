using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

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
                var strSql = Request.QueryString["SQL"];
                var strDb = Request.QueryString["DB"].ToLower();
                DataTable dt;
                switch (strDb)
                {
                    case "sql":
                        dt = ExecuteMssql(strDataSource, strUsername, strPassword, strSql);
                        break;
                    case "oracle":
                        var strServiceName = Request.QueryString["ServiceName"];
                        var intPort = Convert.ToInt16(Request.QueryString["Port"]);
                        dt = ExecuteOracle(strDataSource, intPort, strServiceName, strUsername, strPassword, strSql);
                        break;
                    default:
                        dt = ExecuteMysql(strDataSource, strUsername, strPassword, strSql);
                        break;
                }
                var result = JsonConvert.SerializeObject(dt, Formatting.Indented);
                Response.Write(result);
            }
            catch(Exception ex)
            {
                var result = JsonConvert.SerializeObject(ex, Formatting.Indented);
                Response.Write(result);
            }
        }


        private static DataTable ExecuteMssql(string strDataSource, string strUsername, string strPassword, string strSql)
        {
            const string strDsnTemplate = "Network Library=DBMSSOCN;Data Source={0}; User ID={1}; Password={2};";
            var sqlConnection = string.Format(strDsnTemplate, strDataSource, strUsername, strPassword);
            var adapter = new SqlDataAdapter(strSql, sqlConnection);
            var dataSet = new DataSet();
            adapter.Fill(dataSet, "sql");
            var dt = dataSet.Tables["sql"];
            return dt;
        }


        private static DataTable ExecuteMysql(string strDataSource, string strUsername, string strPassword, string strSql)
        {
            const string strDsnTemplate = "server={0}; uid={1}; pwd={2};";
            var sqlConnection = string.Format(strDsnTemplate, strDataSource, strUsername, strPassword);
            var adapter = new MySqlDataAdapter(strSql, sqlConnection);
            var dataSet = new DataSet();
            adapter.Fill(dataSet, "sql");
            var dt = dataSet.Tables["sql"];
            return dt;
        }

        private static DataTable ExecuteOracle(string strDataSource, int port, string serviceName, string strUsername, string strPassword, string strSQL)
        {
            const string strDsnTemplate = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));user id={3};Password={4}";
            var sqlConnection = string.Format(strDsnTemplate, strDataSource, port, serviceName, strUsername, strPassword);
            var adapter = new OracleDataAdapter(strSQL, sqlConnection) { SelectCommand = { CommandTimeout = 0 } };
            var dataSet = new DataSet();
            adapter.Fill(dataSet, "sql");
            return dataSet.Tables["sql"];
        }
    }
}