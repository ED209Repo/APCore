using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddResturantController : ControllerBase
    {
        //[HttpPost]
        //[Route("Add_Resturant")]

        //public async Task<ActionResult> Add_Resturant([FromBody]AddResturant ad)
        //{
        //    //Add_Resturant 
        //    AddResturant add = new AddResturant();
        //    add.AdResturant(ad);
        //    apResponse<string> response = new apResponse<string>();

        //    if (response.data != null)
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
