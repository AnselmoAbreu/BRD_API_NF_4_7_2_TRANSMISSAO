using System.Net.Http;
using System.Threading.Tasks;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Utils.Extensions
{
	public class ExternalApiRegrasService
	{
		//private static readonly HttpClient client = new HttpClient();

		public async Task<string> CallExternalApiAsync(string apiUrl)
		{
			//var handler = new HttpClientHandler();
			//handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
			HttpClient client = new HttpClient(); // HttpClient(handler)
			try
			{
				HttpResponseMessage response = await client.GetAsync(apiUrl);
				response.EnsureSuccessStatusCode();
				string responseBody = await response.Content.ReadAsStringAsync();
				return responseBody;
			}
			catch (HttpRequestException e)
			{
				return $"Request error: {e.Message} ";
			}
		}
	}
}