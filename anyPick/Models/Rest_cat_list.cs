using System.Data.SqlClient;
using System.Data;

namespace anyPick.Models
{
    public class Rest_cat_list
    {
        public int Rest_Cat_id { get; set; }
        public int Rest_id { get; set; }
        public int Cat_temp_id { get; set; }
        public string Name { get; set; }
        public int Parent_Category { get; set; }
        public List<Food_items> getting_food_Items { get; set; }

        private readonly IConfiguration _config;
      
        public Rest_cat_list(IConfiguration configuration)
        {
            this._config = configuration;
        }
        
      
        public List<Rest_cat_list> Categoryids(int rest_id)
        {
            List<Rest_cat_list> ids = new List<Rest_cat_list>();
            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            bool check = false;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "Select Rest_Cat_id,Rest_id,Cat_temp_id,Name,Parent_Category  from Rest_cat_list where Rest_id='" + rest_id + "'";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Rest_cat_list res = new Rest_cat_list(_config);
                        Food_items s = new Food_items(_config);
                        List<Food_items> O = new List<Food_items>();
                        res.Rest_Cat_id = int.Parse(dt.Rows[i][0].ToString());
                        res.Rest_id = int.Parse(dt.Rows[i][1].ToString());
                        res.Cat_temp_id = int.Parse(dt.Rows[i][2].ToString());
                        res.Name =dt.Rows[i][3].ToString();
                        res.Parent_Category = int.Parse(dt.Rows[i][4].ToString());

                        O = s.foods_items(res.Rest_Cat_id);
                        res.getting_food_Items = O;
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
    public class Resturant_Cataegory
    {
        public int Rest_Cat_id { get; set; }
        public int Rest_id { get; set; }
        public int Cat_temp_id { get; set; }
        public string Name { get; set; }
        public int Parent_Category { get; set; }

        private IConfiguration _config;
        public Resturant_Cataegory(IConfiguration config)
        {
            _config = config;
        }
        public Resturant_Cataegory() 
        {

        }



    }
}
