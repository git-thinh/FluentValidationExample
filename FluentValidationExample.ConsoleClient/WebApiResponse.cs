using System.Net;

namespace FluentValidationExample.ConsoleClient
{
    public class WebApiResponse<T>
    {
        public WebApiResponse(T apiResponse, HttpStatusCode httpStatusCode)
        {
            ApiResponse = apiResponse;
            HttpStatusCode = httpStatusCode;
        }

        public WebApiResponse(string error, HttpStatusCode httpStatusCode, bool isError) // isError is just a way to differentiate the two constructors. If T was a string this constructor would always be called. 
        {
            Error = error;
            HttpStatusCode = httpStatusCode;
        }
        public T ApiResponse { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Error { get; set; }
    }
}
