using System;


namespace EasyBuy2
{
    class Models
    {
    }

    class ProductModel
    {
        public string ActionUrl { get; set; }
        public string authenticity_token { get; set; }
        public string merchant_id { get; set; }
        public string shop_id { get; set; }
        public string item_id { get; set; }
        public string sku { get; set; }
        public string price { get; set; }
        public string currency { get; set; }
        public string variant_id { get; set; }

    }


}