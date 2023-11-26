using anyPick.Authentication_handling;
using anyPick.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace anyPick.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private IConfiguration _config;
        public userController(IConfiguration configuration)
        {
            _config = configuration;

        }

        //Token Generation Method ----------------------------------------------------------------------------------/
        //private string GenerateToken(int Roleid, string Deviceid,int userid)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //      new Claim("Roleid", Roleid.ToString()), // User ID
        //     new Claim("DeviceId", Deviceid), // Device ID
        //      new Claim("userid", userid.ToString())
        //    };

        //    var token = new JwtSecurityToken(
        //        _config["Jwt:Issuer"],
        //        _config["Jwt:Audience"],
        //         claims: claims,
        //         expires: DateTime.Now.AddMinutes(2),
        //         signingCredentials: credentials
          
        //    );

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
       


        //Login EndPoint Method ------------------------------------------------------------------------------------/

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(String Devicid, string? phone, int Roleid, bool Verified)
        {
           AnyPick_user _User = new AnyPick_user(_config);
            Generate_Token gt=new Generate_Token(_config);
           AnyPickUserLogin _UserLogin = new AnyPickUserLogin(_config);

            if (Verified.ToString().Contains("False") && Roleid == 1)
            {
               int userid= _User.getId(Roleid,phone);
                if (userid == 1002)
                {
                    _UserLogin.SetUser_LoginGuest(Roleid,Devicid,userid);
                    var token = gt.GenerateToken(Roleid, Devicid, userid);

                    return StatusCode(StatusCodes.Status200OK,
                                      new apResponse<string> { StatusCode = 200,StatusMessage="Guest User Login SuccessFull",ErrorMessage="Null",data=token });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                                     new apResponse<string> { StatusCode = 404, StatusMessage = "Role Not Found", ErrorMessage = "Null", data = null});
                }
            }
            else if(Verified.ToString().Contains("True") && Roleid == 2)
            {
                int userid=_User.getId(Roleid,phone);
                if (userid == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                                    new apResponse<string> { StatusCode = 404, StatusMessage = "unSuccessFull", ErrorMessage = "UserID Not Found", data = null });
                }
                else
                {
                    _UserLogin.SetUser_LoginRegisterUser(Roleid, Devicid, userid);
                    var token = gt.GenerateToken(Roleid, Devicid, userid);

                    return StatusCode(StatusCodes.Status200OK,
                                    new apResponse<string> { StatusCode = 200, StatusMessage = "RegisterUser Login SuccessFull", ErrorMessage = "Null", data = token });
                }
            }

            return StatusCode(StatusCodes.Status200OK,
                 new apResponse<string> { StatusCode = 204,StatusMessage="No Content Found",ErrorMessage="Invalid Role" ,data= null});
        }



       
        //Signup EndPoint Method------------------------------------------------------------------------------------/

        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> signup([FromBody]AnyPick_user anyPick_User)
        {


            AnyPick_user anyPick_User1 = new AnyPick_user(_config);
            anyPick_User1.RegistratingUser(anyPick_User);
            if (anyPick_User1.UserId == 10)
            {
                return StatusCode(StatusCodes.Status200OK,
                new apResponse<string> { StatusCode = 200, StatusMessage = "User Alerady Exist", ErrorMessage = "Null", data = null });
            }
            else
            {
                return StatusCode(StatusCodes.Status201Created,
                new apResponse<string> { StatusCode = 201, StatusMessage = "User Created SuccessFully", ErrorMessage = "null", data = null });
            }


        }



        //All_Nearby Resturants EndPoint Method ---------------------------------------------------------------------/


        [HttpGet]
        [Route("allnearby_Resturants")]
        public async Task<ActionResult> allnearby_Resturants()  
        {
            Resturant resturant = new Resturant(_config);
           var list= resturant.Getresturants();
            if (list ==null)
            {
                return StatusCode(StatusCodes.Status200OK,
                new apResponse<string> { StatusCode =404 , StatusMessage = "Not Found", ErrorMessage = "No Data Found", data = null });
            }
            else
            {
                //String JsonR = JsonSerializer.Serialize(list);
                return StatusCode(StatusCodes.Status200OK,
                new apResponse<List<Resturant>> { StatusCode = 200, StatusMessage = "OK", ErrorMessage = "", data = list });
            }
           
        }




               























        //Authorization end point check End-Point Ended--------------------------------------------------------------------/
        [Authorize]
        [HttpGet]
        [Route("getDataById")]
        public IActionResult getDataById(int id)
        {
            var ans = "";
            SqlConnection con = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ANYPICK;Data Source=DESKTOP-DEDQ8GT\\SQL");
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                string s1 = "select username from anypickuser where userid='" + id + "'";
                SqlCommand cmd = new SqlCommand(s1, con);
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dr = new DataTable();
                sdr.Fill(dr);
                if (dr.Rows.Count > 0)
                {
                    ans = "user found";
                }
                else
                {
                    ans = "user not Found";
                }
            }
            return Ok(ans);
        }
       


       






    }
}



