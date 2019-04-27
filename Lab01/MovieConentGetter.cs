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
        static string apiCall = "https://api.themoviedb.org/3/movie/550?api_key=13f17cbb56c774d84fe5e497d58fa0dd&fbclid=IwAR3tUd-1wmoQ7p__sz10fltmmeLB-z2QfqVWgjm08jUBHSPexHX9zc7PT48";
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
