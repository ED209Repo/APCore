using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace anyPick.Models
{
    public class Order_Status
    {
        
        public string order_status {  get; set; }

        private readonly IConfiguration _config;
        public Order_Status()
        {

        }
        public Order_Status(IConfiguration configuration)
        {
            this._config = configuration;
        }

        public string OrderStatusUpdate(int id, Order_Status sta)
        {
            try
            {
                bool check = true;
                
                    SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                        string q1 = "select order_id from Order_table where order_id='" + id + "'";
                        SqlCommand cmd = new SqlCommand(q1, con);
                        SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        sdr.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            check = true;
                            con.Close();
                        }
                        else
                        {
                            return "order_id Not Exist";
                        }
                    }
                    else
                    {
                        con.Close();
                    }
                    if (check == true)
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                            string q1 = "UPDATE Order_table SET Order_status='" + sta.order_status + "',Updated_at='"+ DateTime.Now.ToString()+"' WHERE order_id='" + id + "'";
                            SqlCommand cmd = new SqlCommand(q1, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            con.Close();
                        }

                    }
                else
                {
                    return "Please Change order status" ;
                }

                return "Order Status Changed Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

}
