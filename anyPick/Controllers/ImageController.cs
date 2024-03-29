﻿using anyPick.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IConfiguration _config;
        public ImageController(IConfiguration configuration)
        {
            this._config = configuration;
        }




        //Update User-Profile EndPoint Method----------------------------------------------------------------------/
        [HttpPost]
        [Route("uploading_userProfile")]
        public async Task<ActionResult> uploading_userProfile(int id, [FromForm] ImageUpload image)
        {
            ImageUpload imageUpload = new ImageUpload(_config);
            var st = imageUpload.profileImage(id, image);


            if (st == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new apResponse<string> { StatusCode = 400, StatusMessage = "Select Image Plz", ErrorMessage = "", data = null });
            }
            else if (st.Contains("User Id Not Exist"))
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = 204, StatusMessage = "UserId Not EXit", ErrorMessage = "", data = null });
            }
            else if (st.Contains("Plz Upload Image With"))
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string>{ StatusCode = 400,StatusMessage = "Upload Image in jpg,png,jpeg,gif,psd etc ",ErrorMessage = "",data = null});
            }
            else
            {
                return StatusCode(StatusCodes.Status201Created,
                    new apResponse<string> { StatusCode = 201, StatusMessage = "Pics Saved", ErrorMessage = "", data = "Picture Saved Succesfully" });
            }


        }
        //Add Promotional Images
        [HttpPost]
        [Route("Promotional Images")]

        public async Task<ActionResult> Promotional_Images(int id, [FromForm] Promotionalimage image)
        {
            Promotionalimage imageUpload = new Promotionalimage(_config);
            var st = imageUpload.promotionalImage(id, image);


            if (st == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new apResponse<string> { StatusCode = 400, StatusMessage = "Select Image Plz", ErrorMessage = "", data = null });
            }
            else if (st.Contains("RestId Not Exist"))
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = 204, StatusMessage = "RestId Not EXit", ErrorMessage = "", data = null });
            }
            else if (st.Contains("Plz Upload Image With"))
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = 400, StatusMessage = "Upload Image in jpg,png,jpeg,gif,psd etc ", ErrorMessage = "", data = null });
            }
            else
            {
                return StatusCode(StatusCodes.Status201Created,
                    new apResponse<string> { StatusCode = 201, StatusMessage = "Pics Saved", ErrorMessage = "", data = "Picture Saved Succesfully" });
            }
        }
        //Resturant_Logo
        [HttpPost]
        [Route("Resturant logo")]
        public async Task<ActionResult> Resturant_logo(int id, [FromForm]Resturant_Logo image)
        {
            Resturant_Logo imageUpload = new Resturant_Logo(_config);
            var st = imageUpload.Resturant_logoo(id,image);


            if (st == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new apResponse<string> { StatusCode = 400, StatusMessage = "Select Image Plz", ErrorMessage = "", data = null });
            }
            else if (st.Contains("RestId Not Exist"))
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = 204, StatusMessage = "RestId Not EXit", ErrorMessage = "", data = null });
            }
            else if (st.Contains("Plz Upload Image With"))
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = 400, StatusMessage = "Upload Image in jpg,png,jpeg,gif,psd etc ", ErrorMessage = "", data = null });
            }
            else
            {
                return StatusCode(StatusCodes.Status201Created,
                    new apResponse<string> { StatusCode = 201, StatusMessage = "Pics Saved", ErrorMessage = "", data = "Picture Saved Succesfully" });
            }


        }

        //Food Item Image
        [HttpPost]
        [Route("Food_Item_Image")]
        public async Task<ActionResult> FoodItem_Image(int id, [FromForm] FoodItemImage image)
        {
            FoodItemImage imageUpload = new FoodItemImage(_config);
            var st = imageUpload.FoodItem_Image(id, image);


            if (st == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new apResponse<string> { StatusCode = 400, StatusMessage = "Select Image Plz", ErrorMessage = "", data = null });
            }
            else if (st.Contains("Food Item Id Not Exist"))
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = 204, StatusMessage = "Food Item Id Not EXit", ErrorMessage = "", data = null });
            }
            else if (st.Contains("Plz Upload Image With"))
            {
                return StatusCode(StatusCodes.Status200OK,
                    new apResponse<string> { StatusCode = 400, StatusMessage = "Upload Image in jpg,png,jpeg,gif,psd etc ", ErrorMessage = "", data = null });
            }
            else
            {
                return StatusCode(StatusCodes.Status201Created,
                    new apResponse<string> { StatusCode = 201, StatusMessage = "Pics Saved", ErrorMessage = "", data = "Picture Saved Succesfully" });
            }


        }
    }
}
