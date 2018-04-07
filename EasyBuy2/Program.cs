using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Reflection;
using RestSharp;
using HtmlAgilityPack;


namespace EasyBuy2
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();


        static void Main(string[] args)
        {
            long sl = DateTime.Now.Ticks;

            SendToCart();



            long el = DateTime.Now.Ticks;
            long diffL = el - sl;
            Console.WriteLine("Ticks Response from rakuten : {0} ms", (double)diffL / 10000);


            //sl = DateTime.Now.Ticks;
            //GetFormGoogle();
            //el = DateTime.Now.Ticks;
            //diffL = el - sl;
            //Console.WriteLine("Ticks Response from google : {0} ms", (double)diffL / 10000);





            //Console.WriteLine(result);
            Console.ReadKey();

        }


        private static void SendToCart()
        {
            Console.WriteLine("開始取得商品頁面...");
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(GetProductInfo(ConfigurationManager.AppSettings["ProductUrl"]));
            var formData = htmlDocument.GetElementbyId("cart-form");



            Console.WriteLine(formData.Attributes["action"]?.Value);

        }

        //private static void GetForm()
        //{
        //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://www.rakuten.com.tw/");
        //    request.Method = "GET";
        //    //request.Proxy = new WebProxy("127.0.0.1", 8888);
        //    String test = String.Empty;
        //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
        //    {
        //        Stream dataStream = response.GetResponseStream();
        //        StreamReader reader = new StreamReader(dataStream);
        //        test = reader.ReadToEnd();
        //        reader.Close();
        //        dataStream.Close();
        //    }
        //}

        private static string GetProductInfo(string url)
        {
            Console.WriteLine("開始取得商品頁面...");
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            string content;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                content = reader.ReadToEnd();
            }
            return content;
        }


        private static string ProductPagePost()
        {
            Console.WriteLine("傳送POST資料...");
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(ConfigurationManager.AppSettings["url"]);
            string responseContent;
            //System.Net.ServicePointManager.DefaultConnectionLimit = 200;

            ProductModel postDate = new ProductModel()
            {
                authenticity_token = "LTvdN99hu3Ev5e0ECwNB5o8tO7GQmarFEoaGL3USGaU=",
                merchant_id = "e34581b0-ec8b-11e4-979f-005056b74d17",
                shop_id = "2e02b230-ec8d-11e4-ad3d-005056b72eb0",
                item_id = "82ca6880-ed05-11e4-9b71-005056b7650e",
                sku = "100000008863393",
                price = "13980",
                currency = "元",
                variant_id = "82ca6882-ed05-11e4-9b71-005056b7650e",
            };

            httpWebRequest.Method = "POST";    // 方法
            httpWebRequest.KeepAlive = false; //是否保持連線
            httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //httpWebRequest.Proxy = new WebProxy("127.0.0.1", 8888);




            ASCIIEncoding encoding = new ASCIIEncoding();
            StringBuilder parameter = new StringBuilder();

            parameter.Append(string.Format("authenticity_token={0}", postDate.authenticity_token));
            parameter.Append(string.Format("&merchant_id={0}", postDate.merchant_id));
            parameter.Append(string.Format("&shop_id={0}", postDate.shop_id));
            parameter.Append(string.Format("&item_id={0}", postDate.item_id));
            parameter.Append(string.Format("&sku={0}", postDate.sku));
            parameter.Append(string.Format("&price={0}", postDate.price));
            parameter.Append(string.Format("&currency={0}", postDate.currency));
            parameter.Append(string.Format("&variant_id={0}", postDate.variant_id));

            
            byte[] data = Encoding.ASCII.GetBytes(parameter.ToString());
            httpWebRequest.ContentLength = data.Length;
            
            
            using (Stream reqStream = httpWebRequest.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
                        
            using (WebResponse response = httpWebRequest.GetResponse())
            {
                
                using (Stream requestStream = response.GetResponseStream())
                {                   
                    using (StreamReader sr = new StreamReader(requestStream))
                    {
                        
                        responseContent = sr.ReadToEnd();
                        sr.Close();
                    }
                    requestStream.Close();
                }
                response.Close();
            }
            return responseContent;
        }





        //private static void ProductPage()
        //{
        //    HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(ConfigurationManager.AppSettings["url"]);
        //    string responseContent;
        //    //System.Net.ServicePointManager.DefaultConnectionLimit = 200;

        //    ProductModel postDate = new ProductModel()
        //    {
        //        authenticity_token = "LTvdN99hu3Ev5e0ECwNB5o8tO7GQmarFEoaGL3USGaU=",
        //        merchant_id = "e34581b0-ec8b-11e4-979f-005056b74d17",
        //        shop_id = "2e02b230-ec8d-11e4-ad3d-005056b72eb0",
        //        item_id = "82ca6880-ed05-11e4-9b71-005056b7650e",
        //        sku = "100000008863393",
        //        price = "13980",
        //        currency = "元",
        //        variant_id = "82ca6882-ed05-11e4-9b71-005056b7650e",
        //    };

        //    httpWebRequest.Method = "POST";    // 方法
        //    httpWebRequest.KeepAlive = false; //是否保持連線
        //    httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        //    //httpWebRequest.Proxy = new WebProxy("127.0.0.1", 8888);




        //    ASCIIEncoding encoding = new ASCIIEncoding();
        //    StringBuilder parameter = new StringBuilder();

        //    parameter.Append(string.Format("authenticity_token={0}", postDate.authenticity_token));
        //    parameter.Append(string.Format("&merchant_id={0}", postDate.merchant_id));
        //    parameter.Append(string.Format("&shop_id={0}", postDate.shop_id));
        //    parameter.Append(string.Format("&item_id={0}", postDate.item_id));
        //    parameter.Append(string.Format("&sku={0}", postDate.sku));
        //    parameter.Append(string.Format("&price={0}", postDate.price));
        //    parameter.Append(string.Format("&currency={0}", postDate.currency));
        //    parameter.Append(string.Format("&variant_id={0}", postDate.variant_id));

        //    //Console.Write(parameter);

        //    //byte[] data = encoding.GetBytes(parameter.ToString());
        //    //+"&utf8=%E2%9C%93"
        //    byte[] data = Encoding.ASCII.GetBytes(parameter.ToString());
        //    httpWebRequest.ContentLength = data.Length;


        //    long sl = DateTime.Now.Ticks;
        //    using (Stream reqStream = httpWebRequest.GetRequestStream())
        //    {
        //        reqStream.Write(data, 0, data.Length);
        //        reqStream.Close();
        //    }
        //    long el = DateTime.Now.Ticks;
        //    long diffL = el - sl;
        //    Console.WriteLine("Ticks GetRequest : {0} ms", (double)diffL / 10000);

        //    sl = DateTime.Now.Ticks;
        //    using (WebResponse response = httpWebRequest.GetResponse())
        //    {
        //        Console.WriteLine("Ticks GetResponse : {0} ms", (double)(DateTime.Now.Ticks - sl) / 10000);
        //        using (Stream requestStream = response.GetResponseStream())
        //        {
        //            Console.WriteLine("Ticks GetResponseStream : {0} ms", (double)(DateTime.Now.Ticks - sl) / 10000);

        //            using (StreamReader sr = new StreamReader(requestStream))
        //            {
        //                Console.WriteLine("Ticks ReadToEnd : {0} ms", (double)(DateTime.Now.Ticks - sl) / 10000);
        //                responseContent = sr.ReadToEnd();
        //                sr.Close();
        //            }
        //            requestStream.Close();
        //        }
        //        response.Close();
        //    }

        //    el = DateTime.Now.Ticks;
        //    diffL = el - sl;
        //    Console.WriteLine("Ticks Response : {0} ms", (double)diffL / 10000);

        //}





        private static async Task M2Async()
        {
            var value = new Dictionary<string, string> {
                {"authenticity_token", "LTvdN99hu3Ev5e0ECwNB5o8tO7GQmarFEoaGL3USGaU=" }
                ,{ "merchant_id", "e34581b0-ec8b-11e4-979f-005056b74d17"}
                ,{"shop_id", "2e02b230-ec8d-11e4-ad3d-005056b72eb0"}
                ,{"item_id", "82ca6880-ed05-11e4-9b71-005056b7650e"}
                ,{"sku", "100000008863393"}
                ,{"price", "13980"}
                ,{"currency", "元"}
                ,{"variant_id", "82ca6882-ed05-11e4-9b71-005056b7650e"}
            };

            var content = new FormUrlEncodedContent(value);
            long sl = DateTime.Now.Ticks;
            var response = await client.PostAsync(ConfigurationManager.AppSettings["url"], content);

            var responseString = await response.Content.ReadAsStringAsync();
            Console.Write(responseString);
            long el = DateTime.Now.Ticks;
            long diffL = el - sl;
            Console.WriteLine("Ticks GetRequest : {0} ms", (double)diffL / 10000);
        }


    }

    


}






