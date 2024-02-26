using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerConsoleProg
{
    public class HttpService
    {
        private static HttpClient _httpClient=new HttpClient();
        public async Task<int> MakeRequestToGoogle()
        {

            using var activity = ActivitySourceProvider.Source.StartActivity();


            try
            {
                using (var actrivity2 = ActivitySourceProvider.Source.StartActivity("MakewRequestToGoogle > Sahibinden"))
                {
                    actrivity2.AddEvent(new System.Diagnostics.ActivityEvent("google istek baslamadan once"));
                    var responseSah = await _httpClient.GetAsync("https://www.sahibinden.com");
                    actrivity2.AddEvent(new System.Diagnostics.ActivityEvent("google istek biti sonra"));
                    var contentSah = await responseSah.Content.ReadAsStringAsync();
                }

                var response = await _httpClient.GetAsync("https://www.google.com");
                var content = await response.Content.ReadAsStringAsync();

               

                activity.AddTag("content.lenght", content.Length);
                activity.AddTag("user.id", 23);
                activity.AddTag("http.schema", "https");
                return content.Length;
            }
            catch (Exception e)
            {
                activity.SetStatus(System.Diagnostics.ActivityStatusCode.Error);
                Console.WriteLine(e.ToString());
                throw;
            }
            

           
        }

        public async Task<int> MakeRequestToAmazon()
        {
            using var activift=ActivitySourceProvider.Source2.StartActivity();

            var response = await _httpClient.GetAsync("https://www.amazon.com");

            var content= await response.Content.ReadAsStringAsync();
            return content.Length;

        }
    }
}
