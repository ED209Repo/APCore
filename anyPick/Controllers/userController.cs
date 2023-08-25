using anyPick.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Reflection;
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

        //Token Generation Method Started----------------------------------------------------------------------------------/
        private string generateToken(int UserId, string deviceid, int roleid)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])); //
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //Token Generation Method Ended------------------------------------------------------------------------------------/

        //Login EndPoint Method Started------------------------------------------------------------------------------------/
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(String Devicid, string? phone, int Roleid, bool Verified)
        {
           AnyPick_user _User = new AnyPick_user();
           AnyPickUserLogin _UserLogin = new AnyPickUserLogin();

            if (Verified.ToString().Contains("False") && Roleid == 1)
            {
               int userid= _User.getId(Roleid,phone);
                if (userid == 1002)
                {
                    _UserLogin.SetUser_LoginGuest(Roleid,Devicid,userid);
                    var token = generateToken(userid, Devicid, Roleid);

                    return StatusCode(StatusCodes.Status200OK,
                                      new apResponse { StatusCode = 200,StatusMessage="Guest User Login SuccessFull",ErrorMessage="Null",AuthToken=token });
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                                     new apResponse { StatusCode = 404, StatusMessage = "Role Not Found", ErrorMessage = "Null", data = null});
                }
            }
            else if(Verified.ToString().Contains("True") && Roleid == 2)
            {
                int userid=_User.getId(Roleid,phone);
                if (userid == 0)
                {
                    return StatusCode(StatusCodes.Status404NotFound,
                                    new apResponse { StatusCode = 404, StatusMessage = "unSuccessFull", ErrorMessage = "UserID Not Found", data = null });
                }
                else
                {
                    _UserLogin.SetUser_LoginRegisterUser(Roleid, Devicid, userid);
                    var token = generateToken(userid, Devicid, Roleid);

                    return StatusCode(StatusCodes.Status200OK,
                                    new apResponse { StatusCode = 200, StatusMessage = "RegisterUser Login SuccessFull", ErrorMessage = "Null", AuthToken = token });
                }
            }

            return StatusCode(StatusCodes.Status204NoContent,
                 new apResponse { StatusCode = 204,StatusMessage="No Content Found",ErrorMessage="Invalid Role" ,data=null});
        }
        //Login EndPoint Method Ended-------------------------------------------------------------------------------------/



        //Signup EndPoint Method Started------------------------------------------------------------------------------------/
        [HttpPost]
        [Route("Signup")]
        public async Task<IActionResult> Signup([FromBody]AnyPick_user anyPick_User)
        {
            AnyPick_user anyPick_User1 = new AnyPick_user();
            anyPick_User1.RegistratingUser(anyPick_User);
            if (anyPick_User1.UserId == 10)
            {
                return StatusCode(StatusCodes.Status200OK,
                new apResponse { StatusCode = 200, StatusMessage = "User Alerady Created", ErrorMessage = "Null", data = null });
            }
            else
            {
                return StatusCode(StatusCodes.Status201Created,
                new apResponse { StatusCode = 201, StatusMessage = "User Created SuccessFully", ErrorMessage = "null", data = null });
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


