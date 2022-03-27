using System.Net.Http.Headers;

namespace AdminInterface
{
    // To make the class available for each file in the project use tatic
    public static class GlobalVariables
    {
        // HttpCliet provide a class to send/resive Http request/response
        public static HttpClient client = new HttpClient();          // Create an object of HttpClient

        static GlobalVariables()
        {
            client.BaseAddress = new Uri("https://localhost:7249/api/");        // Set API URL
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}