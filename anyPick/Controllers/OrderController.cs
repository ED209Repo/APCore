using anyPick.Authentication_handling;
using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> Creating_Cart(string Token,List<Food_items> itemms)
        {
            Validate_Token _Token = new Validate_Token(_config);
            Cart cart = new Cart(_config);
            apResponse<int> apresponse=new apResponse<int>();

            apresponse=_Token.tokendecode(Token);


            if (apresponse.StatusMessage.Contains("Token Verified SuccessFully"))
            {
                apResponse<string> apresponse1 = new apResponse<string>();
                apresponse1= cart.CreateCart(apresponse.data);

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





    }
}
