using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.SqlClient;
using System.Data;

namespace anyPick.Models
{
    public class Variations
    {
        public int V_Id { get; set; }
        public int food_item_id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Price_Dependent { get; set; }
        public List<Var_Options> Varation_Options { get; set; }


        private readonly IConfiguration _config;
        public Variations()
        {
          
        }
        public Variations(IConfiguration configuration)
        {
            this._config = configuration;
        }



        public List<Variations> GetVariations(int food_item_id)
        {
            List<Variations> ids = new List<Variations>();

            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            bool check = false;
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "Select  V_Id,food_item_id,Name,Type,Price_Dependent from Variations where food_item_id='" + food_item_id + "'";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Variations v = new Variations();
                        Var_Options _Options = new Var_Options(_config);
                        List<Var_Options> s = new List<Var_Options>();

                        v.V_Id = int.Parse(dt.Rows[i][0].ToString());
                        v.food_item_id= int.Parse(dt.Rows[i][1].ToString());
                        v.Name = dt.Rows[i][2].ToString();
                        v.Type = int.Parse(dt.Rows[i][3].ToString());
                        v.Price_Dependent = dt.Rows[i][4].ToString();

                        s=_Options.GetVar_Options(v.V_Id);
                        v.Varation_Options = s;
                        ids.Add(v);
                    }

                    check = true;
                    con.Close();
                }

               

            }
            else
            {
                con.Close ();
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
