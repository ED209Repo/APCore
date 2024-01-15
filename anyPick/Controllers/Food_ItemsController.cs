using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Food_ItemsController : ControllerBase
    {
        private IConfiguration _config;
        public Food_ItemsController(IConfiguration configuration)
        {
            _config = configuration;

        }
        

        [HttpPost]
        [Route("Food_ItemsController")]

        public async Task<ActionResult> Food_Items([FromBody] Add_Food_items ad)
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
                    string q1 = "insert into Food_items values('" + ad.Rest_Cat_id + "','" + ad.Name + "','" + ad.Description + "','" + ad.Unit + "','"+ad.Unit_perPrice+"','"+ad.Prepare_time+"')";
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
                                        new apResponse<string> { StatusCode = 200, StatusMessage = "Food Items Added SuccessFully ", ErrorMessage = "NULL", data = "NULL" }); ;
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK,
                                        new apResponse<string> { StatusCode = 200, StatusMessage = "Food Items Not Added ", ErrorMessage = "NULL", data = "NULL" }); ;
                }
            }


        }


       
        // Add Varaiations

        [HttpPost]
        [Route("Add Varaiations")]

        public async Task<ActionResult> Add_Varaiation([FromBody] Add_Variations adv)
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
                    string q1 = "insert into variations values('" + adv.food_item_id + "','" + adv.Name + "','" + adv.Type + "','" + adv.Price_Dependent + "')";
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
                                        new apResponse<string> { StatusCode = 200, StatusMessage = "Variations Added SuccessFully ", ErrorMessage = "NULL", data = "NULL" }); ;
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK,
                                        new apResponse<string> { StatusCode = 200, StatusMessage = "Variations Not Added ", ErrorMessage = "NULL", data = "NULL" }); ;
                }
            }

        }
        // Add_Varaiation_Option

        [HttpPost]
        [Route("Add_Varaiation_Option")]

        public async Task<ActionResult> Add_Varaiation_Option([FromBody] Add_Variation_Option ado)
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
                    string q1 = "insert into Var_options values('" + ado.V_Id + "','" + ado.Name + "','" + ado.Price_Dependent + "','" + ado.Price + "','" + ado.Add_Price + "','" + ado.Annotation + "')";
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
                                        new apResponse<string> { StatusCode = 200, StatusMessage = "Variation Options Added SuccessFully ", ErrorMessage = "NULL", data = "NULL" }); ;
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK,
                                        new apResponse<string> { StatusCode = 200, StatusMessage = "Variation Options Not Added ", ErrorMessage = "NULL", data = "NULL" }); ;
                }
            }

        }
    }
}

    

