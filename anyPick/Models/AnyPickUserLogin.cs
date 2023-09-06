using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace anyPick.Models
{
    public class AnyPickUserLogin
    {
        public int UserLoginId { get; set; }
        public int RoleId { get; set; }
        public string Deviceid { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public string Lastlogin { get; set; }

        string cs = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ANYPICK;Data Source=DESKTOP-DEDQ8GT\\SQL";

        public void SetUser_LoginGuest(int RoleId, string DeviceId, int UserId)
        {
            SqlConnection con = new SqlConnection(cs);
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "select DeviceId,Status from anyPickuserlogin where DeviceId='" + DeviceId + "'";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][1].ToString().Contains("Expired"))
                    {
                        string q2 = "update anypickuserlogin set lastlogin='" + DateAndTime.Now.ToString() + "' ,Status='Active' where DeviceId='" + DeviceId + "'";
                        SqlCommand cmd1 = new SqlCommand(q2, con);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        string q2 = "update anypickuserlogin set lastlogin='" + DateAndTime.Now.ToString() + "'  where DeviceId='" + DeviceId + "'";
                        SqlCommand cmd1 = new SqlCommand(q2, con);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }

                }
                else
                {
                    string q2 = "insert into anypickuserlogin values('" + RoleId + "','" + DeviceId + "','" + UserId + "','" + "Active" + "','" + DateAndTime.Now.ToString() + "')";
                    SqlCommand cmd1 = new SqlCommand(q2, con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
            }
            else
            {
                con.Close();
            }
        }

        public void SetUser_LoginRegisterUser(int RoleId, string DeviceId, int UserId)
        {
            SqlConnection con = new SqlConnection(cs);
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "select UserId,DeviceId,Status from anyPickuserlogin where UserId='" + UserId + "'";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][1].ToString().Contains(DeviceId))
                    {
                        string q2 = "update anypickuserlogin set lastlogin='" + DateAndTime.Now.ToString() + "' ,Status='Verified' where DeviceId='" + DeviceId + "'";
                        SqlCommand cmd1 = new SqlCommand(q2, con);
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        bool check = true;
                        string q2 = "update anypickuserlogin set Status='Expired'  where UserId='" +UserId+ "' and Deviceid!='"+DeviceId+"'";
                        SqlCommand cmd1 = new SqlCommand(q2, con);
                        cmd1.ExecuteNonQuery();
                        if(check == true)
                        {
                            string q3 = "insert into anypickuserlogin values('" + RoleId + "','" + DeviceId + "','" + UserId + "','" + "Verified" + "','" + DateAndTime.Now.ToString() + "')";
                            SqlCommand cmd2 = new SqlCommand(q3, con);
                            cmd2.ExecuteNonQuery();
                            con.Close();
                        }
                        con.Close();
                    }
                }
                else
                {
                    string q2 = "insert into anypickuserlogin values('" + RoleId + "','" + DeviceId + "','" + UserId + "','" + "Verified" + "','" + DateAndTime.Now.ToString() + "')";
                    SqlCommand cmd1 = new SqlCommand(q2, con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                }
            }
        }


    }
}
