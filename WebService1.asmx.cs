using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace MyServiceExample
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string AddNewUser(string FirstName, string LastName)
        {
            cmd = new SqlCommand("AddNewUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FName", FirstName);
            cmd.Parameters.AddWithValue("@LName", LastName);
            con.Open();
            bool Status = Convert.ToBoolean(cmd.ExecuteNonQuery());
            con.Close();
            if (Status)
            {
                return "Record Saved";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public string UpdateUser(int UserID, string FirstName, string LastName)
        {
            cmd = new SqlCommand("UpdateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", UserID);
            cmd.Parameters.AddWithValue("@FName", FirstName);
            cmd.Parameters.AddWithValue("@LName", LastName);
            con.Open();
            bool Status = Convert.ToBoolean(cmd.ExecuteNonQuery());
            con.Close();
            if (Status)
            {
                return "Record Updated";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public string DeleteUser(int UserID)
        {
            cmd = new SqlCommand("DeleteUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UID", UserID);
            con.Open();
            bool Status = Convert.ToBoolean(cmd.ExecuteNonQuery());
            con.Close();

            if (Status)
            {
                return "Record Deleted";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public DataSet GetAllUsers()
        {
            da = new SqlDataAdapter("GetAllUsers", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            ds = new DataSet();
            da.Fill(ds, "UserInfo");
            return ds;
        }
    }
}
