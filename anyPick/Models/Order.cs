using System.Data.SqlClient;
using System.Data;

namespace anyPick.Models
{
    public class Order
    {
        public int order_id { get; set; }
        public string Order_details { get; set;}
        public int Cart_id { get; set;}
        public string Order_status { get; set;}
        public int rest_id { get; set;}
        public string Payment_status { get; set;}
        public string Order_completion_time { get; set;}
        public int User_id { get;set;}
        public int vechile_id { get; set;}
        public List<Cart> Carts { get; set;}
        

        private readonly IConfiguration _config;
        
        public Order(IConfiguration configuration)
        {
            this._config = configuration;
            Carts = new List<Cart>();
            
        }

        public Order()
        {
        }
        //For GetOrderByRest_Id
        public List<Order> GetOrders(int Rest_id)
        {

            List<Order> Or1 = new List<Order>();
            bool check = false;


            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "SELECT * FROM Order_table WHERE rest_id = '" + Rest_id + "'";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Order res = new Order(_config);
                        Cart s = new Cart(_config);
                        List<Cart> O = new List<Cart>();

                        res.order_id = int.Parse(dt.Rows[i][0].ToString());
                        res.Order_details = dt.Rows[i][1].ToString();
                        res.Cart_id = int.Parse(dt.Rows[i][2].ToString());
                        res.Order_status = dt.Rows[i][3].ToString();
                        res.rest_id = int.Parse(dt.Rows[i][4].ToString());
                        res.Payment_status = dt.Rows[i][5].ToString();
                        res.Order_completion_time = dt.Rows[i][6].ToString();
                        res.User_id = int.Parse(dt.Rows[i][7].ToString());
                        res.vechile_id = int.Parse(dt.Rows[i][8].ToString());


                        O = s.Cartid(res.rest_id);
                        res.Carts = O;
                        Or1.Add(res);
                    }

                    check = true;
                    con.Close();
                }

            }

            if (check == true)
            {
                return Or1;
            }
            else
            {
                return null;
            }



        }

        //For GetOrderByUser_Id

        public List<Order> GetOrders1(int User_id)
        {

            List<Order> Or1 = new List<Order>();
            bool check = false;


            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "SELECT * FROM Order_table WHERE User_id = '" + User_id + "'";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Order res = new Order(_config);
                        Cart s = new Cart(_config);
                        List<Cart> O = new List<Cart>();

                        res.order_id = int.Parse(dt.Rows[i][0].ToString());
                        res.Order_details = dt.Rows[i][1].ToString();
                        res.Cart_id = int.Parse(dt.Rows[i][2].ToString());
                        res.Order_status = dt.Rows[i][3].ToString();
                        res.rest_id = int.Parse(dt.Rows[i][4].ToString());
                        res.Payment_status = dt.Rows[i][5].ToString();
                        res.Order_completion_time = dt.Rows[i][6].ToString();
                        res.User_id = int.Parse(dt.Rows[i][7].ToString());
                        res.vechile_id = int.Parse(dt.Rows[i][8].ToString());


                        O = s.Cartid1(res.User_id);
                        res.Carts = O;
                        Or1.Add(res);
                    }

                    check = true;
                    con.Close();
                }

            }

            if (check == true)
            {
                return Or1;
            }
            else
            {
                return null;
            }



        }

        //For GetOrderByOrder_Id
        public List<Order> GetOrders2(int Order_id)
        {

            List<Order> Or1 = new List<Order>();
            bool check = false;


            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "SELECT * FROM Order_table WHERE order_id = '" + Order_id + "'";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Order res = new Order(_config);
                        Cart s = new Cart(_config);
                        List<Cart> O = new List<Cart>();

                        res.order_id = int.Parse(dt.Rows[i][0].ToString());
                        res.Order_details = dt.Rows[i][1].ToString();
                        res.Cart_id = int.Parse(dt.Rows[i][2].ToString());
                        res.Order_status = dt.Rows[i][3].ToString();
                        res.rest_id = int.Parse(dt.Rows[i][4].ToString());
                        res.Payment_status = dt.Rows[i][5].ToString();
                        res.Order_completion_time = dt.Rows[i][6].ToString();
                        res.User_id = int.Parse(dt.Rows[i][7].ToString());
                        res.vechile_id = int.Parse(dt.Rows[i][8].ToString());


                        O = s.Cartid2(res.order_id);
                        res.Carts = O;
                        Or1.Add(res);
                    }

                    check = true;
                    con.Close();
                }

            }

            if (check == true)
            {
                return Or1;
            }
            else
            {
                return null;
            }



        }






    }
}
