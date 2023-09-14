using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using System.Data.SqlClient;

namespace anyPick.Models
{
    public class Var_Options
    {
        public int V_op_Id { get; set; }
        public int V_Id { get; set; }
        public string Name { get; set; }
        public string Price_Dependent { get; set; }
        public string Price { get; set; }
        public string Add_Price { get; set; }
        public string Annotation { get; set; }

        private readonly IConfiguration _config;
        public Var_Options()
        {
            
        }
        public Var_Options(IConfiguration configuration)
        {
            this._config = configuration;
        }



        public List<Var_Options> GetVar_Options(int V_id)
        {
            List<Var_Options> ids=new List<Var_Options>();
            bool check = false;

            try
            {

                SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "Select  V_op_Id,V_Id,Name,Price_Dependent,Price,Add_Price,Annotation from var_options where V_Id='" + V_id + "'";
                    SqlCommand cmd = new SqlCommand(q1, con);
                    SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    sdr.Fill(dt);
                   if (dt.Rows.Count > 0)
                   {

                     for (int i = 0; i < dt.Rows.Count; i++)
                     {
                          Var_Options v_o = new Var_Options();
                        v_o.V_op_Id = int.Parse(dt.Rows[i][0].ToString());
                        v_o.V_Id = int.Parse(dt.Rows[i][1].ToString());
                        v_o.Name = dt.Rows[i][2].ToString();
                        v_o.Price_Dependent = dt.Rows[i][3].ToString();
                        v_o.Price = dt.Rows[i][4].ToString();
                        v_o.Add_Price = dt.Rows[i][5].ToString();
                        v_o.Annotation = dt.Rows[i][6].ToString();

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
            catch(Exception ex)
            {
                Var_Options msg=new Var_Options();
                msg.Name= ex.Message;
                ids.Add(msg);

                return ids;
            }

           if (check== true)
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
