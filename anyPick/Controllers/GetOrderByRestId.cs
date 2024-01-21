using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrderByRestId : ControllerBase
    {
        private IConfiguration _config;
        public GetOrderByRestId(IConfiguration configuration)
        {
            _config = configuration;

        }

        //GetOrderByRestId
        [HttpGet]
        [Route("GetOrderByRestId")]

       
        public async Task<ActionResult> GetOrderByRestid(int rest_id)
        {
            Order or = new Order(_config);
            var list = or.GetOrders(rest_id);
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
