namespace anyPick.Models
{
    public class Add_Variation_Option
    {
        public int V_op_Id { get; set; }
        public int V_Id { get; set; }
        public string Name { get; set; }
        public string Price_Dependent { get; set; }
        public string Price { get; set; }
        public string Add_Price { get; set; }
        public string Annotation { get; set; }

        private readonly IConfiguration _config;
        public Add_Variation_Option()
        {

        }
        public Add_Variation_Option(IConfiguration configuration)
        {
            this._config = configuration;
        }
    }
}
