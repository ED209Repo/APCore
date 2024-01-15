﻿using Microsoft.AspNetCore.Mvc;
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
                    var fileName = $"{id}{ext}";
                    var filePath = Path.Combine(_uploadpath, fileName);
                    using (Stream stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.File.CopyTo(stream);
                    }
                    
                    
                        if (con.State == System.Data.ConnectionState.Closed)
                        {
                            con.Open();
                            string q1 = "insert into promotionalImgs Values ('" + filePath.ToString() + "' )";
                            SqlCommand cmd = new SqlCommand(q1, con);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            con.Close();
                        }

                    

                }
                else
                {
                    return "Please Upload Promotional Image With" + imageExtensions.ToString();
                }

                return "Image saved Successfully";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


    }
}
