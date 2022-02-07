namespace ChannelEngineLibrary.Model
{
    using System.Net;

    public sealed class ApiResponseModel<T>
    {
        public T Content { get; set; }

        public int Count { get; set; }

        public bool Success { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
