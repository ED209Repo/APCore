namespace anyPick.Models
{
    public class Order
    {
        
        public string Order_details { get; set;}
        public int Cart_id { get; set;}
        public string Order_status { get; set;}
        public int rest_id { get; set;}
        public string Payment_status { get; set;}
        public string Order_completion_time { get; set;}
        public int User_id { get;set;}
        public int vechile_id { get; set;}
        public string Created_At { get; set; }
        public string Updated_At { get; set; }
        private readonly IConfiguration _config;
        public Order()
        {

        }
        public Order(IConfiguration configuration)
        {
            this._config = configuration;
        }







    }
}
