using anyPick.Authentication_handling;
using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> Creating_Cart(string Token)
        {
            Validate_Token _Token = new Validate_Token(_config);
            apResponse<int> apresponse=new apResponse<int>();
          apresponse=_Token.tokendecode(Token);
            if (apresponse.StatusMessage.Contains("Token Verified SuccessFully"))
            {
                return StatusCode(StatusCodes.Status200OK,
                                      new apResponse<int> { StatusCode = apresponse.StatusCode, StatusMessage = apresponse.StatusMessage, ErrorMessage =apresponse.ErrorMessage, data = apresponse.data });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK,
                                      new apResponse<int> { StatusCode = apresponse.StatusCode, StatusMessage = apresponse.StatusMessage, ErrorMessage = apresponse.ErrorMessage, data = apresponse.data });
            }
        }
    }
}
