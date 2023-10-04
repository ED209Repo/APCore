using System.Data.SqlClient;

namespace anyPick.Models
{
    public class Cart
    {


        public int Cart_Id { get; set; }
        public int Order_Id { get; set; }
        public string Cart_status { get; set; }
        public string Payment_status { get; set; }
        public string Price { get; set; }
        public string Total_Price { get; set; }
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        public int UserId { get; set; }
        public int Rest_Id { get; set; }




        private readonly IConfiguration _config;
        public Cart()
        {
            
        }
        public Cart(IConfiguration configuration)
        {
            this._config = configuration;
        }



        public apResponse<string> CreateCart(int id)
        {
            apResponse<string>? apresponse4 =null;
            bool check=false;
            bool check2=false;
            try
            {
                SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "insert into cart values(0,'DRAFT','PENDING','PENDING','PENDING','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + id + "',0)";
                    SqlCommand cmd = new SqlCommand(q1,con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    check = true;
                }

            }
            catch (Exception ex)
            {
                apresponse4 = new apResponse<string>(500, "Internal Server Error", ex.Message, "Null");
                check2= true;
            }


            if (check2 == true)
            {
                return apresponse4;
            }
            else
            {
                if (check == true)
                {
                   apResponse<string> response1=new apResponse<string>(200, "CART CREATED SUCCESSFULLY", "NULL", "NULL");
                    return response1;
                }
                else
                {
                    apResponse<string> response1 = new apResponse<string>(200, "CART  NOT CREATED", "NULL", "NULL");
                    return response1;
                }
            }


        }
    }
}
