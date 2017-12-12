using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MobileSQL
{
    public partial class tableOnly : System.Web.UI.Page
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
                if (strDB == "sql")
                {
                    ExecuteMSSQL(strDataSource, strUsername, strPassword, strSQL);
                }
                else
                {
                    ExecuteMYSQL(strDataSource, strUsername, strPassword, strSQL);
                }
               
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void ExecuteMSSQL(string strDataSource, string strUsername, string strPassword, string strSQL)
        {
            var strDSNTemplate = "Network Library=DBMSSOCN;Data Source={0}; User ID={1}; Password={2};";
            var sqlConnection = string.Format(strDSNTemplate, strDataSource, strUsername, strPassword);
            var adapter = new SqlDataAdapter(strSQL, sqlConnection);
            var dataSet = new DataSet();
            adapter.Fill(dataSet, "sql");
            var dt = dataSet.Tables["sql"];
            this.dgData.DataSource = dt;
            this.dgData.DataBind();
        }


        private void ExecuteMYSQL(string strDataSource, string strUsername, string strPassword, string strSQL)
        {
            var strDSNTemplate = "server={0}; uid={1}; pwd={2};";
            var sqlConnection = string.Format(strDSNTemplate, strDataSource, strUsername, strPassword);
            var adapter = new MySqlDataAdapter(strSQL, sqlConnection);
            var dataSet = new DataSet();
            adapter.Fill(dataSet, "sql");
            var dt = dataSet.Tables["sql"];
            this.dgData.DataSource = dt;
            this.dgData.DataBind();
        }
    }
}