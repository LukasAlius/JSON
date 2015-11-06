using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net.http;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string serviceUrl = string.Format("https://companycheck.p.mashape.com/companySearch?name={0}", "Google");
            var result = getDataFromAPI(serviceUrl).Body;

            var serializer = new JsonSerialisation<List<CompanySearchResult>>();
            var searchResults = serializer.FromString(result);

            Console.WriteLine(searchResults);
        }

        private static HttpResponse<string> getDataFromAPI(string url)
        {
           return Unirest.get(url).asString();  
		}
    }
}
