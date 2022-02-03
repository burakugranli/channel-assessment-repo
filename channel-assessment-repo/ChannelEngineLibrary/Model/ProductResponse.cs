using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngineLibrary.Model
{
    public sealed class ProductResponse
    {
        public string Name { get; set; }

        public string Gtin { get; set; }

        public int TotalQuantity { get; set; }

        public string MerchantProductNo { get; set; }
    }
}
