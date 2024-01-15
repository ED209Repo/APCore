using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResturantCategoryController : ControllerBase
    {
        //Add Resturant Category
        private IConfiguration _config;
        public ResturantCategoryController(IConfiguration configuration)
        {
            _config = configuration;

        }

        [HttpPost]
        [Route("ResturantCategory")]

        public async Task<ActionResult> ResturantCategory([FromBody]Resturant_Cataegory ad)
        {
            apResponse<string>? apresponse4 = null;
            bool check = false;
            bool check2 = false;
            try
            {
                SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "insert into Rest_Cat_List values('" + ad.Rest_id + "','" + ad.Cat_temp_id + "','" + ad.Name + "','" + ad.Parent_Category + "')";
                    SqlCommand cmd = new SqlCommand(q1, con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    check = true;
                }

            }
            catch (Exception ex)
            {
                apresponse4 = new apResponse<string>(500, "Internal Server Error", ex.Message, "Null");
                check2 = true;
            }


            if (check2 == true)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = 200, StatusMessage = "Internal server error", ErrorMessage = "NULL", data = "NULL" });
            }
            else
            {
                if (check == true)
                {

                    return StatusCode(StatusCodes.Status200OK,
                                        new apResponse<string> { StatusCode = 200, StatusMessage = "Category Added SuccessFully ", ErrorMessage = "NULL", data = "NULL" }); ;
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK,
                                        new apResponse<string> { StatusCode = 200, StatusMessage = "Category Not Added ", ErrorMessage = "NULL", data = "NULL" }); ;
                }
            }


        }

        //Search Resturant Category by Rest_Cat_Id
        [HttpGet]
        [Route("ResturantCategory")]

        public async Task<IActionResult> ResturantCategory(int res_Cat_id)
        {
            bool check = false;
            List<Resturant_Cataegory> res = new List<Resturant_Cataegory>();
            SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                string q1 = "SELECT * FROM Rest_Cat_List WHERE Rest_Cat_id = '" + res_Cat_id + "'";

                SqlCommand cmd = new SqlCommand(q1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                sdr.Fill(dt);
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Resturant_Cataegory r = new Resturant_Cataegory(_config);


                        r.Rest_Cat_id = int.Parse(dt.Rows[i][0].ToString());
                        r.Rest_id = int.Parse(dt.Rows[i][1].ToString());
                        r.Cat_temp_id = int.Parse(dt.Rows[i][2].ToString());
                        r.Name = dt.Rows[i][3].ToString();
                        r.Parent_Category = int.Parse(dt.Rows[i][4].ToString());


                        res.Add(r);
                    }

                    check = true;

                }

                con.Close();
            }
            if (check == true)
            {
                return StatusCode(StatusCodes.Status200OK,
                                 new apResponse<List<Resturant_Cataegory>> { StatusCode = 200, StatusMessage = "Resturant_Cataegory Founds", ErrorMessage = "", data = res });

            }
            else
            {
                return StatusCode(StatusCodes.Status200OK,
             new apResponse<string> { StatusCode = 200, StatusMessage = "Resturant_Cataegory Not Found", ErrorMessage = "", data = "Null" });

            }
        }
    }

    
}
