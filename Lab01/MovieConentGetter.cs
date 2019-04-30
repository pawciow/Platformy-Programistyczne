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
        static string apiCall = "https://api.themoviedb.org/3/discover/movie?api_key=13f17cbb56c774d84fe5e497d58fa0dd";
        public static async Task<RootObject> GetApiAsync()
        {
            RootObject random = null;
 /*           while (true)
            {*/
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(apiCall + "&page=" + GetRandomNumber() ))
                using (HttpContent content = response.Content)
                {
                    var stringContent = await content.ReadAsStringAsync();
                    random = JsonConvert.DeserializeObject<RootObject>(stringContent);
                }
                Thread.Sleep(1000);
           // }
            return random;
        }
        private static int GetRandomNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 1000);
            return randomNumber;
        }
    }
}
