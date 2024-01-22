using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusUpdate : ControllerBase
    {
        private readonly IConfiguration _config;
        public OrderStatusUpdate(IConfiguration configuration)
        {
            this._config = configuration;
        }
        [HttpPost]
        [Route("OrderStatusUpdate")]

        public async Task<ActionResult> OrderStatus_Update(int id, Order_Status sta)
        {
            Order_Status Order_Stat = new Order_Status(_config);
            var st = Order_Stat.OrderStatusUpdate(id, sta);


             if (st.Contains("order_id Not Exist"))
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = 204, StatusMessage = "order_id Not Exist", ErrorMessage = "", data = null });
            }
            
            else
            {
                return StatusCode(StatusCodes.Status201Created,
                    new apResponse<string> { StatusCode = 201, StatusMessage = "Order Status Saved", ErrorMessage = "", data = "Order Status Changed Succesfully" });
            }
        }
    }
}
