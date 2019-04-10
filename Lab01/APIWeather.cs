using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
namespace Lab01
{
    static class OpenWeatherContentGetter
    {
        static string apiKey = "1b6714e500f0cdd864a8b49ec6ac5e45"; ///@todo: change to my API
        static string apiBaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        private static string buildCityLink(string city)
        {
            return apiBaseUrl + "?q=" + city + "&units=metric" + "&apikey=" + apiKey + "&mode=xml";
        }

        public static async Task<string> getCityWeatherContentAsStringAsync(string city)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(buildCityLink(city)))
            using (HttpContent content = response.Content)
            {
                return await content.ReadAsStringAsync();
            }
        }

        public static Weather Parse(System.IO.Stream stream)
        {
            XElement xml = XElement.Load(stream);
            var nameQuery = (from element in xml.Elements()
                             let elementName = element.Name
                             where (elementName == "city")
                             select new
                             {
                                 City = element.Attributes("name").FirstOrDefault(),
                             });
            var temperatureQuery = (from element in xml.Elements()
                                    let elementName = element.Name
                                    where (elementName == "temperature")
                                    select new
                                    {
                                        Temperature = element.Attributes("value").FirstOrDefault(),
                                    });
            return new Weather()
            {
                City = nameQuery.FirstOrDefault().City.Value,
                Temperature = float.Parse(
                    temperatureQuery.FirstOrDefault().Temperature.Value,
                    System.Globalization.CultureInfo.InvariantCulture)
            };
        }
    }
}
