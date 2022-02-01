using System;
using System.Text;
using System.Collections.Generic;
using RestSharp;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Reflection;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            //getExchangeData();
            Console.WriteLine("Enter First Currency Code :");

            string currencyone = Console.ReadLine();//usd
            Console.WriteLine("Enter Second Currency Code :");

            string currencytwo = Console.ReadLine();//bdt
            Console.WriteLine("Enter Amount :");

            double amount = Convert.ToDouble(Console.ReadLine());//100

            Console.WriteLine("Enter Date (Optional):");

            DateTime inputdate = DateTime.Parse(Console.ReadLine());
            string date = inputdate.ToString("yyyy-MM-dd");
            using (var client = new HttpClient())
            {

                var endpoint = new Uri("http://data.fixer.io/api/latest?access_key=2b316116d60a0a99000d853f1715bf7e");
                var result = client.GetAsync(endpoint).Result;
                var jsonString = result.Content.ReadAsStringAsync().Result;


                JObject obj = JsonConvert.DeserializeObject<JObject>(jsonString);
                JObject innerObj = obj["rates"] as JObject;
                var first = innerObj.ContainsKey(currencyone);
                var second = innerObj.ContainsKey(currencytwo);

                var firstRate = innerObj[currencyone];
                var secondRate = innerObj[currencytwo];


                var sec = (double)secondRate;
                var firs = (double)firstRate;

                if (firs < sec)
                {
                    var div = (sec / firs) * amount;
                    Console.WriteLine(div.ToString("#.##"));
                }
                else
                {
                    var seDiv = (firs * amount) / sec;
                    Console.WriteLine(seDiv.ToString("#.##"));
                }

                if (date != null)
                {
                    var opendpoint = new Uri("http://data.fixer.io/api/" + date + "?access_key=2b316116d60a0a99000d853f1715bf7e");
                    var opresult = client.GetAsync(opendpoint).Result;
                    var opjsonString = opresult.Content.ReadAsStringAsync().Result;


                    JObject opobj = JsonConvert.DeserializeObject<JObject>(opjsonString);
                    JObject opinnerObj = opobj["rates"] as JObject;

                    var opfirst = opinnerObj.ContainsKey(currencyone);
                    var opsecond = opinnerObj.ContainsKey(currencytwo);

                    var opfirstRate = opinnerObj[currencyone];
                    var opsecondRate = opinnerObj[currencytwo];

                    var opsec = (double)opsecondRate;
                    var opfirs = (double)opfirstRate;

                    if (opfirs < opsec)
                    {
                        var opdiv = (opsec / opfirs) * amount;
                        Console.WriteLine(opdiv.ToString("#.##"));
                    }
                    else
                    {
                        var opseDiv = (opfirs * amount) / opsec;
                        Console.WriteLine(opseDiv.ToString("#.##"));
                    }

                }

            }
        }

        //public static void getExchangeData()
        //{
        //    var client = new RestClient("http://data.fixer.io/api/latest?access_key=2b316116d60a0a99000d853f1715bf7e");
        //    var request = new RestRequest("latest");

        //    var response = client.ExecuteGetAsync(request);

        //    //string rawresponse = response.content;

        //}
    }
}
