using Microsoft.VisualBasic;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Crm.Sdk.Messages;

namespace anyPick.Models
{
    public class AnyPick_user
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Dob { get; set; }
        public string CreatedAt { get; set; }
        public string LocationLongitude { get; set; }
        public string LocationLatitude { get; set; }
        public int ActivationStatus { get; set; }
        public string ProfileImage { get; set; }
        public int RoleId { get; set; }
        public string authToken { get; set; }

        public int getId(int roleid, string phone)
        {
            bool check = false;
            int id = 0;
            if (roleid == 1)
            {
                return (1002);
            }
            else
            {
               
                SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ANYPICK;Data Source=DESKTOP-DEDQ8GT\\SQL");
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "select UserId from AnyPickUser where phone='"+phone+"'";
                    SqlCommand cmd = new SqlCommand(q1, con);
                    SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    sdr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        id=int.Parse(dt.Rows[0][0].ToString());
                        con.Close();
                        check= true;  
                    }
                   
                }
                else
                {
                    con.Close();
                }


                if (check == true)
                {
                    return id;
                }
                else
                {
                    return id;
                }
                
            }
        }

        public void RegistratingUser(AnyPick_user anyPick_User)
        {
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ANYPICK;Data Source=DESKTOP-DEDQ8GT\\SQL");
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "select phone from anyPickuser where phone='" + anyPick_User.phone + "'";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    UserId = 10;
                    con.Close();
                }
                else
                {
                   
                    string q2 = "insert into anyPickuser  values('" + anyPick_User.Email + "','" + anyPick_User.phone + "','" + anyPick_User.UserName + "','" + anyPick_User.Address + "','" + anyPick_User.Gender + "','" + anyPick_User.Dob + "','" + DateAndTime.Now + "','" + anyPick_User.LocationLongitude + "','" + anyPick_User.LocationLatitude + "','1','empty','" + anyPick_User.RoleId + "')";
                    SqlCommand cmd1 = new SqlCommand(q2, con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    UserId = 11;
                }
            }
            else
            {
                con.Close();
            }
        }







    }
}
