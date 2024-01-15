using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetVechileController : ControllerBase
    {
        private IConfiguration _config;
        public GetVechileController(IConfiguration configuration)
        {
            _config = configuration;

        }

        //Get_Vechile

        [HttpGet]
        [Route("Get_Vechile")]

        public async Task<IActionResult> Get_Vechile(int Userid)
        {
            bool check = false;
            List<Cars> vec = new List<Cars>();
            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                string q1 = "SELECT * FROM Vechiles WHERE [User id] = '" + Userid + "'";


                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cars r = new Cars(_config);

                        r.Vechid = int.Parse(dt.Rows[i][0].ToString());
                        r.VechileName = dt.Rows[i][1].ToString();
                        r.Color = dt.Rows[i][2].ToString();
                        r.VechileRegistrationNumber = dt.Rows[i][3].ToString();
                        r.VechileCompany = dt.Rows[i][4].ToString();
                        r.Userid = int.Parse(dt.Rows[i][5].ToString());


                        vec.Add(r);
                    }

                    check = true;

                }

                con.Close();
            }
            if (check == true)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<List<Cars>> { StatusCode = 200, StatusMessage = "Vechiles Found", ErrorMessage = "", data = vec });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = 200, StatusMessage = " No Vechiles Found", ErrorMessage = "", data = "Null" });
            }
        }
    }
}
