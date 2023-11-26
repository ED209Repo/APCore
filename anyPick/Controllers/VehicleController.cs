using anyPick.Authentication_handling;
using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private IConfiguration _config;
        public VehicleController(IConfiguration configuration)
        {
            _config = configuration;

        }

        [HttpPost]
        [Route("Add_Vechile")]
        public async Task<ActionResult> Add_Vechile([FromBody] Cars ve, string Token)
        {
            Validate_Token _Token = new Validate_Token(_config);
            apResponse<int> apresponse = new apResponse<int>();

            apresponse = _Token.tokendecode(Token);

            if (apresponse.StatusMessage.Contains("Token Verified SuccessFully"))
            {
                Cars vs = new Cars(_config);
                int vechilestatus = vs.AddingVechile(ve);


                if (vechilestatus == 1)
                {
                    return StatusCode(StatusCodes.Status200OK,
                                     new apResponse<string> { StatusCode = 200, StatusMessage = "Vechile successFully Created", ErrorMessage = "", data = "" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK,
                                     new apResponse<string> { StatusCode = 500, StatusMessage = "Vechile Not Created", ErrorMessage = "Internal Server Error", data = "" });
                }


            }
            else
            {
                return StatusCode(StatusCodes.Status200OK,
                                      new apResponse<int> { StatusCode = apresponse.StatusCode, StatusMessage = apresponse.StatusMessage, ErrorMessage = apresponse.ErrorMessage, data = apresponse.data });
            }

        }
    }
}
