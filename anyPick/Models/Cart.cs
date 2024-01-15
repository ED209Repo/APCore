using System.Data;
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
        public int UserId { get; set; }
        public int Rest_Id { get; set; }

        public List<Cart_Items> Cart_Items {  get; set; }


        private readonly IConfiguration _config;
        public Cart()
        {
            
        }
        public Cart(IConfiguration configuration)
        {
            this._config = configuration;
            //Cart_Items = new List<Cart_Items>();
        }



        public apResponse<string> CreateCart(int id,int Rest_id)
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
                    string q1 = "insert into cart values(0,'DRAFT','PENDING','PENDING','PENDING','" + DateTime.Now.ToString() + "','" + DateTime.Now.ToString() + "','" + id + "','"+Rest_id+"')";
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
                    
                    apResponse<string> response1 = new apResponse<string>(200, "CART CREATED SUCCESSFULLY, Rest_id: " + Rest_id, "NULL",Rest_id.ToString());

                    return response1;
                }
                else
                {
                    apResponse<string> response1 = new apResponse<string>(200, "CART  NOT CREATED", "NULL", "NULL");
                    return response1;
                }
            }


        }

        //For GetOrderByRest_Id
        public List<Cart> Cartid(int Rest_id)
        {
            List<Cart> ids = new List<Cart>();
            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            bool check = false;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "SELECT Cart_Id,Order_Id,Cart_status,Payment_status,Price,Total_Price,UserId,Rest_Id FROM Cart WHERE Rest_Id =  '"+ Rest_id + "' " ;
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cart res = new Cart(_config);
                        Cart_Items s = new Cart_Items(_config);
                        List<Cart_Items> O = new List<Cart_Items>();

                        res.Cart_Id = int.Parse(dt.Rows[i][0].ToString());
                        res.Order_Id = int.Parse(dt.Rows[i][1].ToString());
                        res.Cart_status = dt.Rows[i][2].ToString();
                        res.Payment_status = dt.Rows[i][3].ToString();
                        res.Price = dt.Rows[i][4].ToString();
                        res.Total_Price = dt.Rows[i][5].ToString();
                        res.UserId = int.Parse(dt.Rows[i][6].ToString());
                        res.Rest_Id = int.Parse(dt.Rows[i][7].ToString());

                        O = s.GetCart_items(res.Cart_Id);
                        res.Cart_Items = O;
                        ids.Add(res);
                    }

                    check = true;
                    con.Close();
                }

            }
            else
            {
                con.Close();
            }



            if (check == true)
            {
                return ids;
            }
            else
            {
                return null;
            }
        }

        //For GetOrderByUser_Id
        public List<Cart> Cartid1(int User_id)
        {
            List<Cart> ids = new List<Cart>();
            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            bool check = false;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "SELECT Cart_Id,Order_Id,Cart_status,Payment_status,Price,Total_Price,UserId,Rest_Id FROM Cart WHERE UserId =  '" + User_id + "' ";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cart res = new Cart(_config);
                        Cart_Items s = new Cart_Items(_config);
                        List<Cart_Items> O = new List<Cart_Items>();

                        res.Cart_Id = int.Parse(dt.Rows[i][0].ToString());
                        res.Order_Id = int.Parse(dt.Rows[i][1].ToString());
                        res.Cart_status = dt.Rows[i][2].ToString();
                        res.Payment_status = dt.Rows[i][3].ToString();
                        res.Price = dt.Rows[i][4].ToString();
                        res.Total_Price = dt.Rows[i][5].ToString();
                        res.UserId = int.Parse(dt.Rows[i][6].ToString());
                        res.Rest_Id = int.Parse(dt.Rows[i][7].ToString());

                        O = s.GetCart_items1(res.Cart_Id);
                        res.Cart_Items = O;
                        ids.Add(res);
                    }

                    check = true;
                    con.Close();
                }

            }
            else
            {
                con.Close();
            }



            if (check == true)
            {
                return ids;
            }
            else
            {
                return null;
            }
        }

        //For GetOrderByOrder_Id
        public List<Cart> Cartid2(int Order_id)
        {
            List<Cart> ids = new List<Cart>();
            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            bool check = false;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "SELECT Cart_Id,Order_Id,Cart_status,Payment_status,Price,Total_Price,UserId,Rest_Id FROM Cart WHERE order_id =  '" + Order_id + "' ";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cart res = new Cart(_config);
                        Cart_Items s = new Cart_Items(_config);
                        List<Cart_Items> O = new List<Cart_Items>();

                        res.Cart_Id = int.Parse(dt.Rows[i][0].ToString());
                        res.Order_Id = int.Parse(dt.Rows[i][1].ToString());
                        res.Cart_status = dt.Rows[i][2].ToString();
                        res.Payment_status = dt.Rows[i][3].ToString();
                        res.Price = dt.Rows[i][4].ToString();
                        res.Total_Price = dt.Rows[i][5].ToString();
                        res.UserId = int.Parse(dt.Rows[i][6].ToString());
                        res.Rest_Id = int.Parse(dt.Rows[i][7].ToString());

                        O = s.GetCart_items2(res.Cart_Id);
                        res.Cart_Items = O;
                        ids.Add(res);
                    }

                    check = true;
                    con.Close();
                }

            }
            else
            {
                con.Close();
            }



            if (check == true)
            {
                return ids;
            }
            else
            {
                return null;
            }
        }
    }
}
