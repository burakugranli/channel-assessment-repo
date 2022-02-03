using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineLibrary.Model
{
    public sealed class ApiResponseModel<T>
    {
        public IEnumerable<T> Content { get; set; }

        public int Count { get; set; }

        public bool Success { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
