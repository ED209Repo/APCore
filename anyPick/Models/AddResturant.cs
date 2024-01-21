using System.Data.SqlClient;

namespace anyPick.Models
{
    public class AddResturant
    {
       
        public string Resturant_Name { get; set; }
        public string Open_Close_Time { get; set; }
        public string Offdays { get; set; }
        public string Resturant_type { get; set; }
        public string business_Id { get; set; }
        public string licesnes_Id { get; set; }
        public string promotional_Image {  get; set; }
        public string Res_Logo { get; set; }

        private readonly IConfiguration _config;
        public AddResturant()
        {

        }
        public AddResturant(IConfiguration configuration)
        {
            this._config = configuration;
        }

        



    }
}
