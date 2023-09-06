namespace anyPick.Models
{
    public class apResponse<T>
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string ErrorMessage { get; set; }
        public  T data { get; set; }
       

    } 
}
