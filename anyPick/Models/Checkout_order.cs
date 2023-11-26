using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace anyPick.Models
{
    public class Checkout_order
    {
        public int restId { get; set; } 
        public int cartId { get; set; }
        public int PaymentId { get; set; }
        public string Payment_status { get; set; }
        public string Payment_Type { get; set; }

        private readonly IConfiguration _config;
        public Checkout_order()
        {

        }
        public Checkout_order(IConfiguration configuration)
        {
            this._config = configuration;
        }
        public apResponse<string> Checkout([FromBody]Checkout_order Co)
        {
            apResponse<string>? apresponse4 = null;
            bool check = false;
            bool check2 = false;
            try
            {
                SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "insert into checkout_order values('"+Co.cartId+"','"+Co.restId+"','"+Co.Payment_status+"','"+Co.PaymentId+"','"+Co.Payment_Type+"')";
                    SqlCommand cmd = new SqlCommand(q1, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    check = true;
                }

            }
            catch (Exception ex)
            {
                apresponse4 = new apResponse<string>(500, "Internal Server Error", ex.Message, "Null");
                check2 = true;
            }


            if (check2 == true)
            {
                return apresponse4;
            }
            else
            {
                if (check == true)
                {

                    apResponse<string> response1 = new apResponse<string>(200, "Checkout SUCCESSFULLY, " , "NULL", "NULL");

                    return response1;
                }
                else
                {
                    apResponse<string> response1 = new apResponse<string>(200, "No Checkout ", "NULL", "NULL");
                    return response1;
                }
            }
        }
    }
}
