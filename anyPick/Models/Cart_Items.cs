using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace anyPick.Models
{
    public class Cart_Items
    {
        public int Cart_Item_Id { get; set; }
        public int Cart_Id { get; set; }
        public int Food_Item_id { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }
        public string Total_Prce { get; set; }
        public int Deal_Id { get; set; }
        public int V_Id { get; set; }
        public int V_op_Id { get; set; }
        public string Created_At { get; set; }

        private readonly IConfiguration _config;

        public Cart_Items()
        {

        }
        
        public Cart_Items(IConfiguration configuration)
        {
            this._config = configuration;
        }

        public apResponse<string> creatingcart(List<Food_items> items,int cartid)
        {
            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));

            if (items == null)
            {
                apResponse<string> response= new apResponse<string>(404, "FOOD ITEMS EMPTY", "NULL", null);
                
                return response;
            }
            else
            {
                //CREATING  CART FOR FOOD ITEMS
                for (int i = 0; i < items.Count; i++)
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        string q1 = "insert into cart_Items values('" + cartid + "','" + items[i].Food_Item_id +"','"+ items[i].Unit_perPrice + "','"+ items[i].Unit + "','"+ items[i].Unit_perPrice + "',0,0,0,'"+DateTime.Now.ToString()+"','FALSE' )";
                        SqlCommand cmd = new SqlCommand(q1,con);
                        cmd.ExecuteNonQuery();

                        con.Close();
                    }

                    //CREATING CART FOR VARIATIONS
                    for(int j=0; j < items[i].variations.Count; j++)
                    {
                        for (int k=0; k < items[i].variations[j].Varation_Options.Count; k++)
                        {
                            //CREATING CART FOR PRICE DEPENDENT VARIATIONS 
                            if (items[i].variations[j].Varation_Options[k].Price_Dependent.Equals("True", StringComparison.InvariantCultureIgnoreCase))
                            {
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                    string q1 = "insert into cart_Items values('" + cartid + "','" + items[i].Food_Item_id + "','" + items[i].variations[j].Varation_Options[k].Add_Price + "','1','" +( int.Parse(items[i].variations[j].Varation_Options[k].Price)+int.Parse(items[i].variations[j].Varation_Options[k].Add_Price)).ToString() + "',0,'" + items[i].variations[j].Varation_Options[k].V_Id + "','" + items[i].variations[j].Varation_Options[k].V_op_Id + "','" + DateTime.Now.ToString() + "','TRUE' )";
                                    SqlCommand cmd = new SqlCommand(q1, con);
                                    cmd.ExecuteNonQuery();

                                    con.Close();
                                }
                            }
                            else
                            {
                                //CREATING CART FOR NON-PRICE DEPENDENT VARIATIONS 
                                if (con.State == ConnectionState.Closed)
                                {
                                    con.Open();
                                    string q1 = "insert into cart_Items values('" + cartid + "','" + items[i].Food_Item_id + "','0','1','0',0,'" + items[i].variations[j].Varation_Options[k].V_Id + "','" + items[i].variations[j].Varation_Options[k].V_op_Id + "','" + DateTime.Now.ToString() + "','TRUE' )";
                                    SqlCommand cmd = new SqlCommand(q1, con);
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                        }
                    }


                }
                apResponse<string> response = new apResponse<string>(200, "CART ITEM SUCCESSFULLY SAVED", "NULL", null);

                return response;
            }
            
        }

        //For GetOrderByRest_Id
        public List<Cart_Items> GetCart_items(int Cart_id)
        {
            List<Cart_Items> ids = new List<Cart_Items>();
            bool check = false;

            try
            {

                SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "SELECT * FROM Cart_Items WHERE Cart_Id =   '"+Cart_id+"' ";
                    SqlCommand cmd = new SqlCommand(q1, con);
                    SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    sdr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Cart_Items v_o = new Cart_Items();
                            v_o.Cart_Item_Id = int.Parse(dt.Rows[i][0].ToString());
                            v_o.Cart_Id = int.Parse(dt.Rows[i][1].ToString());
                            v_o.Food_Item_id = int.Parse(dt.Rows[i][2].ToString());
                            v_o.Price = dt.Rows[i][3].ToString();
                            v_o.Quantity = dt.Rows[i][4].ToString();
                            v_o.Total_Prce = dt.Rows[i][5].ToString();
                            v_o.Deal_Id = int.Parse(dt.Rows[i][6].ToString());
                            v_o.V_Id = int.Parse(dt.Rows[i][7].ToString());
                            v_o.V_op_Id = int.Parse(dt.Rows[i][8].ToString());

                            ids.Add(v_o);
                        }

                        check = true;
                        con.Close();
                    }

                }
                else
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Cart_Items msg = new Cart_Items();
                msg.Quantity = ex.Message;
                ids.Add(msg);

                return ids;
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

        public List<Cart_Items> GetCart_items1(int Cart_id)
        {
            List<Cart_Items> ids = new List<Cart_Items>();
            bool check = false;

            try
            {

                SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "SELECT * FROM Cart_Items WHERE Cart_Id =   '" + Cart_id + "' ";
                    SqlCommand cmd = new SqlCommand(q1, con);
                    SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    sdr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Cart_Items v_o = new Cart_Items();
                            v_o.Cart_Item_Id = int.Parse(dt.Rows[i][0].ToString());
                            v_o.Cart_Id = int.Parse(dt.Rows[i][1].ToString());
                            v_o.Food_Item_id = int.Parse(dt.Rows[i][2].ToString());
                            v_o.Price = dt.Rows[i][3].ToString();
                            v_o.Quantity = dt.Rows[i][4].ToString();
                            v_o.Total_Prce = dt.Rows[i][5].ToString();
                            v_o.Deal_Id = int.Parse(dt.Rows[i][6].ToString());
                            v_o.V_Id = int.Parse(dt.Rows[i][7].ToString());
                            v_o.V_op_Id = int.Parse(dt.Rows[i][8].ToString());

                            ids.Add(v_o);
                        }

                        check = true;
                        con.Close();
                    }

                }
                else
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Cart_Items msg = new Cart_Items();
                msg.Quantity = ex.Message;
                ids.Add(msg);

                return ids;
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

        public List<Cart_Items> GetCart_items2(int Cart_id)
        {
            List<Cart_Items> ids = new List<Cart_Items>();
            bool check = false;

            try
            {

                SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "SELECT * FROM Cart_Items WHERE Cart_Id =   '" + Cart_id + "' ";
                    SqlCommand cmd = new SqlCommand(q1, con);
                    SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    sdr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Cart_Items v_o = new Cart_Items();
                            v_o.Cart_Item_Id = int.Parse(dt.Rows[i][0].ToString());
                            v_o.Cart_Id = int.Parse(dt.Rows[i][1].ToString());
                            v_o.Food_Item_id = int.Parse(dt.Rows[i][2].ToString());
                            v_o.Price = dt.Rows[i][3].ToString();
                            v_o.Quantity = dt.Rows[i][4].ToString();
                            v_o.Total_Prce = dt.Rows[i][5].ToString();
                            v_o.Deal_Id = int.Parse(dt.Rows[i][6].ToString());
                            v_o.V_Id = int.Parse(dt.Rows[i][7].ToString());
                            v_o.V_op_Id = int.Parse(dt.Rows[i][8].ToString());

                            ids.Add(v_o);
                        }

                        check = true;
                        con.Close();
                    }

                }
                else
                {
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Cart_Items msg = new Cart_Items();
                msg.Quantity = ex.Message;
                ids.Add(msg);

                return ids;
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
