using System.Data.SqlClient;
using System.Data;

namespace anyPick.Models
{
    public class FoodItemImage
    {
        public IFormFile File { get; set; }

        public FoodItemImage()
        {

        }
        public FoodItemImage(IConfiguration configuration)
        {
            _config = configuration;
        }

        private readonly IConfiguration _config;


        public string FoodItem_Image(int id, FoodItemImage image)
        {
            bool check = false;
            String FileName = image.File.FileName;
            String ext = Path.GetExtension(FileName);
            String[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".tif", ".psd", ".svg" };
            string _uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "Pics\\FoodItemImage");


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
                        string q1 = "select FoodItemImg from Food_items where Food_item_id='" + id + "'";
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
                            return "Food_item_id Not Exist";
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
                            string q1 = "UPDATE Food_items SET FoodItemImg='" + filePath.ToString() + "' WHERE Food_item_id='" + id + "'";
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
                    return "Please Upload Food Item Image With" + imageExtensions.ToString();
                }

                return "Food Item Image saved Successfully";

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}
