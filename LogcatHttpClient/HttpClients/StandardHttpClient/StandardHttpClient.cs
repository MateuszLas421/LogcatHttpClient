using LogcatHttpClient.HttpClients.Refit;
using Newtonsoft.Json;
using System.Text;

namespace LogcatHttpClient.HttpClients.StandardHttpClient;

public class StandardHttpClient
{
    private readonly HttpClient _httpClient;
    string url;
    public StandardHttpClient(string url)
    {
        _httpClient = new HttpClient();
        this.url = url;
    }

    public async Task<string> SendAuthRequest(Input input)
    {
        try
        {
            string jsonContent = JsonConvert.SerializeObject(input);

            HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync($"{url}/test", content);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResult>(responseContent);
                if (!string.IsNullOrEmpty(result?.result))
                    return await Task.FromResult(" HttpClient Success ");
            }
            else
            {
                return await Task.FromResult($"Error: {response?.StatusCode} " +
                    $"ReasonPhrase: {response?.ReasonPhrase}" +
                    $" Content: {response?.Content}" +
                    $" StatusCode: {response?.StatusCode}" +
                    $" Version: {response?.Version}"
                    );
            }
        }
        catch (Exception ex)
        {
            return await Task.FromResult($"Exception: {ex.Message}");
        }
        return await Task.FromResult($"Exception SendAuthRequest: Send error");
    }
}