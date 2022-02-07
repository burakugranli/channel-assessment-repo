namespace ChannelEngineLibrary.Model
{
    using System.Collections.Generic;

    public sealed class Product
    {
        public bool IsActive { get; set; }

        public string MerchantProductNo { get; set; }

        public IEnumerable<ExtraData> ExtraData { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public string Ean { get; set; }

        public string ManufacturerProductNumber { get; set; }

        public int Stock { get; set; }

        public double Price { get; set; }

        public double MSRP { get; set; }

        public double PurchasePrice { get; set; }

        public string VatRateType { get; set; }

        public double ShippingCost { get; set; }

        public string ShippingTime { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public string ImageUrl1 { get; set; }

        public string ImageUrl2 { get; set; }

        public string ImageUrl3 { get; set; }

        public string ImageUrl4 { get; set; }

        public string ImageUrl5 { get; set; }

        public string ImageUrl6 { get; set; }

        public string ImageUrl7 { get; set; }

        public string ImageUrl8 { get; set; }

        public string ImageUrl9 { get; set; }

        public string CategoryTrail { get; set; }
    }
}
