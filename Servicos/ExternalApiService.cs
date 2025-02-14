using System.Net.Http;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Servicos
{
	public class ExternalApiService
	{
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> CallExternalApiAsync(string apiUrl)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                // Handle the exception as needed
                return $"Request error: {e.Message}";
            }
        }
    }
}