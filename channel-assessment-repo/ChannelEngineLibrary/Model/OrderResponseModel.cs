using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineLibrary.Model
{
    public sealed class OrderResponseModel
    {
        public IEnumerable<Order> Content { get; set; }
    }
}
