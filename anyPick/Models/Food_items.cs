using System.Data.SqlClient;
using System.Data;

namespace anyPick.Models
{
    public class Food_items
    {
        public int Food_Item_id { get; set; }
        public int Rest_Cat_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Unit_perPrice { get; set; }
        public string Prepare_time { get; set; }
        public List<Variations> variations { get; set; }



        private readonly IConfiguration _config;

        public Food_items()
        {

        }
        public Food_items(IConfiguration configuration)
        {
            this._config = configuration;
        }



        public List<Food_items> foods_items(int Rest_Cat_id)
        {
            List<Food_items> ids = new List<Food_items>();
            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            bool check = false;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "Select  food_item_id,Rest_Cat_id,name,description_,unit,unit_perprice,prepare_time  from Food_items where Rest_Cat_id='" + Rest_Cat_id + "'";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Food_items item = new Food_items(_config);
                        Variations s = new Variations(_config);
                        List<Variations> O = new List<Variations>();

                        item.Food_Item_id = int.Parse(dt.Rows[i][0].ToString());
                        item.Rest_Cat_id = int.Parse(dt.Rows[i][1].ToString());
                        item.Name = dt.Rows[i][2].ToString();
                        item.Description = dt.Rows[i][3].ToString();
                        item.Unit = dt.Rows[i][4].ToString();
                        item.Unit_perPrice = dt.Rows[i][5].ToString();
                        item.Prepare_time = dt.Rows[i][6].ToString();

                        O = s.GetVariations(item.Food_Item_id);
                        item.variations = O;
                        ids.Add(item);
                    }

                    check = true;
                    con.Close();
                }

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


