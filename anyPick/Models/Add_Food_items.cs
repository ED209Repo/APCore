using System.Data;
using System.Data.SqlClient;

namespace anyPick.Models
{
    public class Add_Food_items
    {
        public int Food_Item_id { get; set; }
        public int Rest_Cat_id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Unit_perPrice { get; set; }
        public string Prepare_time { get; set; }

        


        private readonly IConfiguration _config;

        public Add_Food_items()
        {

        }
        public Add_Food_items(IConfiguration configuration)
        {
            this._config = configuration;
        }

    }
}
