using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using Microsoft.VisualBasic;


namespace WEBSERVICEHRD
{
    /// <summary>
    /// Summary description for HRDService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class HRDService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetUserJSon(string username,string userpassword )
        {

            string dEUserName = "";
            string dEmpCode = "";
            string dEmpName = "";
           
            int dEmpId = 0;
            bool dAuthentication = false ;
            bool dAuthenusername = false;
            bool dAuthenpassword = false;

            string connectstring = "SERVER = MAIN-WSM-DB\\MN; UID = sa; PWD =5k,mew,; Initial Catalog = HITECH_SYSTEM";
            string stringcmd = "";
            stringcmd = "select top 1 *  FROM [HITECH_SECURITY].dbo.TSEUserLogin where FTUserName='" + username  + "'";
            DataCon Conn = new WEBSERVICEHRD.DataCon();

            System.Data.DataTable dt = Conn.GetDataTable(stringcmd, connectstring);

            if (dt.Rows.Count > 0) {

                dAuthenusername = true ;

                if (HI.Conn.DB.FuncDecryptData(dt.Rows[0]["FTPassword"].ToString()) == userpassword) {
                           dAuthenpassword = true;
                            dEmpId= (int)(Microsoft.VisualBasic.Conversion.Val(dt.Rows[0]["FNHsysEmpId"].ToString()) );
                    dAuthentication = true;
                } 
               

            } else {

                dAuthenusername = false;
                dAuthenpassword = false;

            };

            UserDataInfo emps = new UserDataInfo {   EmpId = dEmpId
                                                    , Authentication = dAuthentication
                                                    , Authenusername = dAuthenusername
                                                    , Authenpassword = dAuthenpassword
            };
           
            return new JavaScriptSerializer().Serialize(emps);

        }

    }

    public class UserDataInfo
    {
        public bool Authentication { get; set; }
        public bool Authenusername { get; set; }
        public bool Authenpassword { get; set; }
        public int EmpId { get; set; }
        
    }

    public class DataCon {

    

        public System.Data.DataTable GetDataTable(string QryStr, string _ConnectionString, string TableName = "DataTalble1")
        {
            System.Data.DataTable objDT = new System.Data.DataTable(TableName);
            System.Data.SqlClient.SqlConnection _Cnn = new System.Data.SqlClient.SqlConnection();
            System.Data.SqlClient.SqlCommand _Cmd = new System.Data.SqlClient.SqlCommand();

            try
            {
                string  _ConnString = _ConnectionString;

                if (_Cnn.State == System.Data.ConnectionState.Open) { _Cnn.Close(); };
                _Cnn.ConnectionString = _ConnectionString;
                _Cnn.Open();
                _Cmd = _Cnn.CreateCommand();

                var _Adepter = new System.Data.SqlClient.SqlDataAdapter(_Cmd);
                _Adepter.SelectCommand.CommandTimeout = 0;
                _Adepter.SelectCommand.CommandType = System.Data.CommandType.Text;
                _Adepter.SelectCommand.CommandText = QryStr;
                _Adepter.Fill(objDT);
                _Adepter.Dispose();

                DisposeSqlConnection(_Cmd);
                DisposeSqlConnection(_Cnn);

            }
            catch (Exception ex)
            {
                DisposeSqlConnection(_Cmd);
                DisposeSqlConnection(_Cnn);
            }

            return objDT;
        }

        public  void DisposeSqlConnection(System.Data.SqlClient.SqlConnection _cnn)
        {
            if ((_cnn != null))
            {
                if (_cnn.State == System.Data.ConnectionState.Open)
                {
                    _cnn.Close();
                }
                _cnn.Dispose();
            }
        }

        public  void DisposeSqlConnection(System.Data.SqlClient.SqlCommand _cmd)
        {
            if ((_cmd != null))
            {
                if ((_cmd.Connection != null))
                {
                    if (_cmd.Connection.State == System.Data.ConnectionState.Open)
                    {
                        _cmd.Connection.Close();
                    }
                    _cmd.Connection.Dispose();
                }
                _cmd.Dispose();
            }
        }
    }

   


}
