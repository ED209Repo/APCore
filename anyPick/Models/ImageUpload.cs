﻿using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace anyPick.Models
{
    public class ImageUpload
    {
        public IFormFile file { get; set; }

        string cs = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ANYPICK;" +
            "Data Source=DESKTOP-DEDQ8GT\\SQL";

        public ImageUpload()
        {
            //string _uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "profile_Pic");//Profile 
        }

        private readonly string _uploadpath;
        public string profileImage(int id,ImageUpload image)
        {
            bool chec=false;
            int stor_id;
            if (image == null)
            {
                return null;
            }


           
            try
            {
                string _uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "Pics/profile_Pic");//Profile 
                SqlConnection con = new SqlConnection(cs);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                    string q1 = "select Userid from AnyPickuser where userid='"+ id + "'";
                    SqlCommand cmd = new SqlCommand(q1, con);
                    SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    sdr.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                       chec= true;
                        con.Close();
                    }
                    else
                    {
                        return "User Id Not Exist";
                        
                    }

                }
                else
                {
                    con.Close() ;
                }

                var fileName = $"{id}.{"jpg"}";
                var filePath = Path.Combine(_uploadpath,fileName);

                using (Stream stream=new FileStream(filePath, FileMode.Create))
                {
                    image.file.CopyTo(stream);
                }

                if (chec = true)
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
                return "Pic saved Successfully";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
