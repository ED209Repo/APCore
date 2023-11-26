using System.Data.SqlClient;

namespace anyPick.Models
{
    public class Cars
    {
        public int Vechid { get; set; }

        public string VechileName { get; set; }
        public string Color { get; set; }
        public string VechileRegistrationNumber { get; set; }
        public string VechileCompany { get; set; }
        public int Userid { get; set; }


        private readonly IConfiguration _config;
        public Cars(IConfiguration configuration)
        {
            this._config = configuration;

        }
        public Cars( )
        {
            

        }



        public int AddingVechile(Cars vechile)
        {
            bool check = false;
            {
                SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "insert into Vechiles values('" + vechile.VechileName + "','" + vechile.Color + "','" + vechile.VechileRegistrationNumber + "','" + vechile.VechileCompany + "','" + vechile.Userid + "')";
                    SqlCommand cmd = new SqlCommand(q1, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    check = true;
                }
                if (check == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
