using System.Net;

namespace CRUDVueNet1Back.Utilities
{
    public class ApiResponse<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessage { get; set; } = [];
        public T? Result { get; set; }
    }
}
