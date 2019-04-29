using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
namespace Lab01
{
    class MovieConentGetter
    {
        static string apiCall = "https://api.themoviedb.org/3/discover/movie?certification_country=US&certification=R&sort_by=revenue.desc&with_cast=3896";
        public static async Task<GetRandomMovie> GetApiAsync()
        {
            GetRandomMovie random = null;
 /*           while (true)
            {*/
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(apiCall))
                using (HttpContent content = response.Content)
                {
                    var stringContent = await content.ReadAsStringAsync();
                    random = JsonConvert.DeserializeObject<GetRandomMovie>(stringContent);
                }
                Thread.Sleep(1000);
           // }
            return random;
        }
    }
}
