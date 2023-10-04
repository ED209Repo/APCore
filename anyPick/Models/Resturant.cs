using Microsoft.AspNetCore.Http.HttpResults;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace anyPick.Models
{
    public class Resturant
    {
        public int Rest_Id { get; set; }
        public string Resturant_Name { get; set; }
        public string Open_Close_Time { get; set;}
        public string Offdays { get; set; }
        public string Resturant_type { get; set; }
        public string business_Id { get; set; }
        public string licesnes_Id { get; set; }
        public string Created_At { get; set; }
        public List<Rest_cat_list>  Rest_Cat_ { get; set; }

        private readonly IConfiguration _config;
       
        public Resturant(IConfiguration configuration)
        {
            _config = configuration;
            Rest_Cat_ = new List<Rest_cat_list>();
        }

       

        public List<Resturant> Getresturants()
        {
           
            List<Resturant> resturants = new List<Resturant>();
            bool check = false;


            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
                string q1 = "select * from Resturant";
                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        Resturant res = new Resturant(_config);
                        Rest_cat_list s = new Rest_cat_list(_config);
                        List<Rest_cat_list> O = new List<Rest_cat_list>();
                       
                        res.Rest_Id = int.Parse(dt.Rows[i][0].ToString());
                        res.Resturant_Name = dt.Rows[i][1].ToString();
                        res.Open_Close_Time = dt.Rows[i][2].ToString();
                        res.Offdays = dt.Rows[i][3].ToString();
                        res.Resturant_type = dt.Rows[i][4].ToString();
                        res.business_Id = dt.Rows[i][5].ToString();
                        res.licesnes_Id = dt.Rows[i][6].ToString();
                        res.Created_At = dt.Rows[i][7].ToString();

                      
                        O = s.Categoryids(res.Rest_Id);
                        res.Rest_Cat_ = O;
                        resturants.Add(res);
                    }

                    check = true;
                  con.Close();
                }
               
            }

            if (check == true)
            {
                return resturants;
            }
            else
            {
                return null;
            }
            
           
            
        }

      

    }
}
