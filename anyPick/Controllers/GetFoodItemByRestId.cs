using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetFoodItemByRestId : ControllerBase
    {
        private IConfiguration _config;
        public GetFoodItemByRestId(IConfiguration configuration)
        {
            _config = configuration;

        }
        [HttpGet]
        [Route("GetFoodItemByRestId")]

        public async Task<ActionResult> GetFoodItemByRestid(int Rest_id)
        {
            Resturant or = new Resturant(_config);
            var list = or.Getresturants1(Rest_id);
            if (list == null)
            {
                return StatusCode(StatusCodes.Status200OK,
                new apResponse<string> { StatusCode = 404, StatusMessage = "Not Found", ErrorMessage = "No Data Found", data = null });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK,
                new apResponse<List<Resturant>> { StatusCode = 200, StatusMessage = "OK", ErrorMessage = "", data = list });
            }

        }
    }
}
