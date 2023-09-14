using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace anyPick.Models
{
    public class ImageUpload
    {
        public IFormFile file { get; set; }


        private readonly IConfiguration _config;
        // public ImageUpload(IConfiguration configuration)
        //{
        //     file = null;
        //     this._config = configuration;
        //}


        public string profileImage(int id, ImageUpload image)
        {
            bool chec = false;
            String FileName = image.file.FileName;
            String ext = Path.GetExtension(FileName);
            String[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".tif", ".psd", ".svg" };
            string _uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "Pics\\profile_Pic");
            string cs = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ANYPICK;Data Source=DESKTOP-DEDQ8GT\\SQL";
            if (image == null)
            {
                return null;
            }

            try
            {
                if (imageExtensions.Contains(ext))
                {
                    SqlConnection con = new SqlConnection(cs);
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                        string q1 = "select Userid from AnyPickuser where userid='" + id + "'";
                        SqlCommand cmd = new SqlCommand(q1, con);
                        SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        sdr.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            chec = true;
                            con.Close();
                        }
                        else
                        {
                            return "User Id Not Exist";
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
                        image.file.CopyTo(stream);
                    }
                    if (chec == true)
                    {
                        if (con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                            string q1 = "update anypickuser set profileimage='" + filePath.ToString() + "' where userid='" + id + "'";
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
                    return "Plz Upload Image With" + imageExtensions.ToString();
                }

                return "Pic saved Successfully";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


    }
}
