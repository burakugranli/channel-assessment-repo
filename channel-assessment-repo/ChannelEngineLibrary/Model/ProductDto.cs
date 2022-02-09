namespace ChannelEngineLibrary.Model
{
    public sealed class ProductDto
    {
        public string Name { get; set; }

        public string Gtin { get; set; }

        public int TotalQuantity { get; set; }

        public string MerchantProductNo { get; set; }

        public int Stock { get; set; }
    }
}
