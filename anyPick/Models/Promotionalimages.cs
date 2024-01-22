using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using ConfigurationManager = Microsoft.Extensions.Configuration.ConfigurationManager;

namespace anyPick.Models
{
    public class Promotionalimage
    {
        public IFormFile File { get; set; }

        public Promotionalimage()
        {

        }
        public Promotionalimage(IConfiguration configuration)
        {
            _config = configuration;
        }

        private readonly IConfiguration _config;


        public string promotionalImage(int id, Promotionalimage image)
        {
            bool check = false;
            String FileName = image.File.FileName;
            String ext = Path.GetExtension(FileName);
            String[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".tif", ".psd", ".svg" };
            string _uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "Pics\\PromotionalImgs");


            if (image == null)
            {
                return null;
            }

            try
            {
                if (imageExtensions.Contains(ext))
                {
                    SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnStr"));
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                        string q1 = "select Rest_id from Resturant where Rest_id='" + id + "'";
                        SqlCommand cmd = new SqlCommand(q1, con);
                        SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        sdr.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            check = true;
                            con.Close();
                        }
                        else
                        {
                            return "RestId Not Exist";
                        }
                    }
                    else
                    {
                        con.Close();
                    }

                    var fileName = $"{id}{ext}";
                    var filePath = Path.Combine(_uploadpath, fileName);
                    using (Stream stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.File.CopyTo(stream);
                    }
                    if (check == true)
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                            string q1 = "UPDATE Resturant SET promotional_Images='" + filePath.ToString() + "' WHERE Rest_id='" + id + "'";
                            SqlCommand cmd = new SqlCommand(q1, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            con.Close();
                        }

                    }

                }
                else
                {
                    return "Please Upload promotional Image With" + imageExtensions.ToString();
                }

                return "promotional Image saved Successfully";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


    }
}
