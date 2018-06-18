using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PlayingWithAsync
{
    public class Program
    {
        public static readonly string BaseUrl_NASA = "https://api.nasa.gov/planetary/apod?api_key=DEMO_KEY";
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Тоже самое что Task.Result только чуть безопаснее
            //var res = Task.Run(() => GetUrl()).GetAwaiter().GetResult();
            // https://cpratt.co/async-tips-tricks/
            var res = GetUrl().GetAwaiter().GetResult();
            Console.WriteLine(res);
        }

        public static async Task<string> GetUrl()
        {
            using(var client = new HttpClient())
            {
                using(var response = await client.GetAsync(BaseUrl_NASA).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                }
            }
        }

    }
}
