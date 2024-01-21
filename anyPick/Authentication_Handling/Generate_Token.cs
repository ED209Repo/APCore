using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace anyPick.Authentication_handling
{
    public class Generate_Token
    {
        private IConfiguration _config;
        public Generate_Token(IConfiguration configuration)
        {
            _config = configuration;

        }

        //Token Generation Method ----------------------------------------------------------------------------------/
        public string GenerateToken(int Roleid, string Deviceid, int userid)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
              new Claim("Roleid", Roleid.ToString()), // User ID
             new Claim("DeviceId", Deviceid), // Device ID
              new Claim("userid", userid.ToString())
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                 claims: claims,
                 expires: DateTime.Now.AddMinutes(2),
                 signingCredentials: credentials

            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
