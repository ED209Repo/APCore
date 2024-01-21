using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrderByUserId : ControllerBase
    {

        private IConfiguration _config;
        public GetOrderByUserId(IConfiguration configuration)
        {
            _config = configuration;

        }

        //GetOrderByUserId

        [HttpGet]
        [Route("GetOrderByUserId")]

        public async Task<ActionResult> GetOrderByUserid(int User_id)
        {
            Order or = new Order(_config);
            var list = or.GetOrders1(User_id);
            if (list == null)
            {
                return StatusCode(StatusCodes.Status200OK,
                new apResponse<string> { StatusCode = 404, StatusMessage = "Not Found", ErrorMessage = "No Data Found", data = null });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK,
                new apResponse<List<Order>> { StatusCode = 200, StatusMessage = "OK", ErrorMessage = "", data = list });
            }

        }
    }
}
