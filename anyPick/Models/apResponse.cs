namespace anyPick.Models
{
    public class apResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string data { get; set; }
        public string AuthToken { get; set; }

    } 
}
