using System.Data.SqlClient;
using System.Data;
using System.Drawing;

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

         



    }
}
