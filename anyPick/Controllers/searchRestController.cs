using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class searchRestController : ControllerBase
    {
        private IConfiguration _config;
        public searchRestController(IConfiguration configuration)
        {
            _config = configuration;

        }

        [HttpGet]
        [Route("Search_Resturant")]

        public async Task<IActionResult> Search_Resturant(string resname)
        {
            bool check = false;
            List<Resturant> res = new List<Resturant>();
            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                string q1 = "SELECT * FROM Resturant WHERE Resturant_Name LIKE '%" + resname.ToString() + "%'";

                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                   
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Resturant r = new Resturant(_config);
                       
                       

                        r.Rest_Id= int.Parse(dt.Rows[i][0].ToString());
                        r.Resturant_Name= dt.Rows[i][1].ToString();
                        r.Open_Close_Time= dt.Rows[i][2].ToString();
                        r.Offdays= dt.Rows[i][3].ToString();
                        r.Resturant_type= dt.Rows[i][4].ToString();
                        r.business_Id= dt.Rows[i][5].ToString();
                        r.licesnes_Id= dt.Rows[i][6].ToString();
                        r.Created_At= dt.Rows[i][7].ToString();

                        res.Add(r);
                    }

                    check = true;
                    
                }

                con.Close();
            }
            if (check == true)
            {
                return StatusCode(StatusCodes.Status200OK,
                                 new apResponse<List<Resturant>> { StatusCode = 200, StatusMessage = "Resturant Founds", ErrorMessage = "", data = res });

            }
            else
            {
                return StatusCode(StatusCodes.Status200OK,
             new apResponse<string> { StatusCode = 200, StatusMessage = "Resturant Not Found", ErrorMessage = "", data = "Null" });

            }
        }
    }
}
