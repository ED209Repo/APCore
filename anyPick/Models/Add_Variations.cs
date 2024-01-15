namespace anyPick.Models
{
    public class Add_Variations
    {
        public int V_Id { get; set; }
        public int food_item_id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Price_Dependent { get; set; }
        


        private readonly IConfiguration _config;
        public Add_Variations()
        {

        }
        public Add_Variations(IConfiguration configuration)
        {
            this._config = configuration;
        }
    }
}
