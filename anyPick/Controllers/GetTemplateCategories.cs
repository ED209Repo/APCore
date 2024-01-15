using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace anyPick.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]

    public class GetTemplateCategories : ControllerBase
    {
        private IConfiguration _config;
        public GetTemplateCategories(IConfiguration configuration)
        {
            _config = configuration;

        }
        [HttpGet]
        [Route("GetTemplateCategories")]

        public async Task<IActionResult> GetTemplateCategory()
        {
            bool check = false;
            List<Category_temp> ct = new List<Category_temp>();
            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                string q1 = "SELECT * FROM Category_temp";

                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Category_temp c1 = new Category_temp(_config);


                        c1.Cat_temp_id = int.Parse(dt.Rows[i][0].ToString());
                        c1.Cat_name = dt.Rows[i][1].ToString();
                        c1.Cat_description = dt.Rows[i][2].ToString();
                   
                        ct.Add(c1);
                    }

                    check = true;

                }

                con.Close();
            }
            if (check == true)
            {
                return StatusCode(StatusCodes.Status200OK,
                                 new apResponse<List<Category_temp>> { StatusCode = 200, StatusMessage = "Category Founds", ErrorMessage = "", data = ct });

            }
            else
            {
                return StatusCode(StatusCodes.Status200OK,
             new apResponse<string> { StatusCode = 200, StatusMessage = "Category Not Found", ErrorMessage = "", data = "Null" });

            }
        }
    }
}
