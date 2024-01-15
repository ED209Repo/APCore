namespace anyPick.Models
{
    public class Category_temp
    {
        public int Cat_temp_id { get; set; }
        public string Cat_name { get; set; }
        public string Cat_description { get;set; }

        private IConfiguration _config;
        public Category_temp(IConfiguration config)
        {
            _config = config;
        }
    }
    


}
