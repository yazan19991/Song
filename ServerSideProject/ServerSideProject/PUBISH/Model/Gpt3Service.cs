using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ServerSideProject.Model
{
    public class Gpt3Service
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public Gpt3Service(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }


        /// Gets the GPT3 completion. This is a blocking call and will return once the user has finished typing a prompt
        /// 
        /// @param prompt - The prompt to display to the user
        /// 
        /// @return A Gpt3Response object with the result of the request or null if the request failed for any
        public async Task<Gpt3Response> GetCompletion(string prompt)
        {
            //var apiKey = _configuration["OpenAiOptions:MY_OPEN_AI_API_KEY"];
            var apiKey = "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";
            var endpoint = "https://api.openai.com/v1/engines/davinci/completions";

            var requestContent = new
            {
                prompt = prompt,
                max_tokens = 50
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var response = await _httpClient.PostAsync(endpoint, content);


            /// Returns the response body as a Gpt3Response object.
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Gpt3Response>(responseContent);
            }
            else
            {
                return null;
            }
        }
    }
}
