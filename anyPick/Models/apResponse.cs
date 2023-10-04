using Microsoft.AspNetCore.Http;

namespace anyPick.Models
{
    public class apResponse<T>
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string ErrorMessage { get; set; }
        public  T data { get; set; }


        public apResponse()
        {

        }
        public apResponse(int statuscode, string statusmessage, string errormessage, T data)
        {
            StatusCode = statuscode;
            StatusMessage = statusmessage;
            ErrorMessage = errormessage;
            data = this.data;
        }

       




    } 
}
