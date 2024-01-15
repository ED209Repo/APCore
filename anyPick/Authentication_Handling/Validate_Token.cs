using anyPick.Models;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace anyPick.Authentication_handling
{
    public class Validate_Token
    {
        private IConfiguration _config;
        public Validate_Token(IConfiguration configuration)
        {
            _config = configuration;

        }




        private ClaimsPrincipal? checking_ClaimsPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    return null;
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims, "jwt"));

                return claimsPrincipal;
            }
            catch (Exception ex)
            {
                return new ClaimsPrincipal(new ClaimsIdentity(ex.Message));
            }
        }

        public apResponse<int> tokendecode(string token)
        {
            bool check = false;
            bool check1 = false;
            List<object> t = new List<object>();
            apResponse<int> ap = new apResponse<int>(); 
            apResponse<int> ap1 = new apResponse<int>(); 
            apResponse<int> ap2 = new apResponse<int>();
            ClaimsPrincipal claimsPrincipal = checking_ClaimsPrincipal(token);

            if (claimsPrincipal != null)
            {
                foreach (Claim claim in claimsPrincipal.Claims)
                {
                    t.Add($"{claim.Type}: {claim.Value}");
                }
                var id = t[2].ToString();
                string[] splitting = id.Split(":");
                int u_id = int.Parse(splitting[1].ToString());

                try
                {
                    SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                        string q1 = "Select  userid from AnyPickUser where userid='" + u_id + "'";
                        SqlCommand cmd = new SqlCommand(q1, con);
                        SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        sdr.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            ap = new apResponse<int>();
                            ap.StatusCode = 200;
                            ap.StatusMessage = "Token Verified SuccessFully";
                            ap.ErrorMessage = "null";
                            ap.data = u_id;

                            check = true;
                        }
                        else
                        {

                            ap1.StatusCode = 200;
                            ap1.StatusMessage = "Token Not Verified ";
                            ap1.ErrorMessage = "Wrong User Credentials";
                            ap1.data = 0;
                        }
                    }
                }
                catch (Exception ex)
                {

                    ap.StatusCode = 500;
                    ap.StatusMessage = "Internal Server Error";
                    ap.ErrorMessage = ex.Message;
                    ap.data = 0;
                }
            }
            else
            {

                ap2.StatusCode = 200;
                ap2.StatusMessage = "";
                ap2.ErrorMessage = "Credentials Not Found";
                ap2.data = 0;

                check1 = true;
            }

            if (check1 == true)
            {
                return ap2;
            }
            else
            {
                if (check == true)
                {
                    return ap;
                }
                else
                {
                    return ap1;
                }

            }
        }
    }
}
