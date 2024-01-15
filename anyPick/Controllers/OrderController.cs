using anyPick.Authentication_handling;
using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IConfiguration _config;
        public OrderController(IConfiguration configuration)
        {
            _config = configuration;

        }


        [HttpPost]
        [Route("Creating_Cart")]

        //creating cart 
        public async Task<ActionResult> Creating_Cart(string Token,int Rest_Id)
        {
            Validate_Token _Token = new Validate_Token(_config);
          
            Cart cart = new Cart(_config);
            apResponse<int> apresponse=new apResponse<int>();

            apresponse=_Token.tokendecode(Token);


            if (apresponse.StatusMessage.Contains("Token Verified SuccessFully"))
            {
                apResponse<string> apresponse1 = new apResponse<string>();
                apresponse1= cart.CreateCart(apresponse.data,Rest_Id);

                if (apresponse1.StatusMessage.Contains("CART CREATED SUCCESSFULLY"))
                {
                    return StatusCode(StatusCodes.Status200OK,
                                     new apResponse<string> { StatusCode = apresponse1.StatusCode, StatusMessage =apresponse1.StatusMessage, ErrorMessage = apresponse1.ErrorMessage, data = apresponse1.data });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK,
                                      new apResponse<string> { StatusCode = apresponse1.StatusCode, StatusMessage = apresponse1.StatusMessage, ErrorMessage = apresponse1.ErrorMessage, data = apresponse1.data });
                }


            }
            else
            {
                return StatusCode(StatusCodes.Status200OK,
                                      new apResponse<int> { StatusCode = apresponse.StatusCode, StatusMessage = apresponse.StatusMessage, ErrorMessage = apresponse.ErrorMessage, data = apresponse.data });
            }
        }


        [HttpPost]
        [Route("CartItems")]
        public async Task<ActionResult> creating_Cart_Items(List<Food_items> items,int cartid)
        {
            //creating cart_items 
            Cart_Items cart = new Cart_Items(_config);
            apResponse<string> response = new apResponse<string>();
            response=cart.creatingcart(items,cartid);

            if (response.data != null)
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = response.StatusCode, StatusMessage = response.StatusMessage, ErrorMessage = response.ErrorMessage, data =response.data });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = response.StatusCode, StatusMessage = response.StatusMessage, ErrorMessage = response.ErrorMessage, data = response.data });
            }

        }

        [HttpPost]
        [Route("creating_order")]
        //creating order 
        public async Task<ActionResult> creating_order([FromBody]Order v)
        {
            
            apResponse<string> apresponse4;
            bool check = false;
            bool check2 = false;
            try
            {
                SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "insert into Order_table values('"+v.Order_details+"','"+v.Cart_id+"','Pending','"+v.rest_id+"','"+v.Payment_status+"','---','"+v.User_id+"','"+v.vechile_id+ "','"+DateTime.Now.ToString()+ "','" +DateTime.Now.ToString()+"')";
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
                    new apResponse<string> { StatusCode=200, StatusMessage = "Internal server error", ErrorMessage = "NULL", data = "NULL" }); 
            }
            else
            {
                if (check == true)
                {

                    return StatusCode(StatusCodes.Status200OK,
                                        new apResponse<string> { StatusCode = 200, StatusMessage = "Order Created ", ErrorMessage = "NULL", data = "NULL" }); ;
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK,
                     new apResponse<string> { StatusCode = 200, StatusMessage = " order not created", ErrorMessage = "NULL", data = "NULL" }); ;
                }
            }


        }

        //[HttpPost]
        //[Route("Checkout_order")]
        ////CheckOut Order
        //public Task<ActionResult> Checkout_order()
        //{
        //    Checkout_order cc = new Checkout_order();
        //    apResponse<string> response = new apResponse<string>();
        //    response cc.Checkout(apresponse.data);
        //    if (apresponse.data != null)
        //    {
        //        return StatusCode(StatusCodes.Status200OK,
        //            new apResponse<string> { StatusCode = response.StatusCode, StatusMessage = response.StatusMessage, ErrorMessage = response.ErrorMessage, data = response.data });
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status200OK,
        //            new apResponse<string> { StatusCode = response.StatusCode, StatusMessage = response.StatusMessage, ErrorMessage = response.ErrorMessage, data = response.data });
        //    }

        //}


    }



    }

