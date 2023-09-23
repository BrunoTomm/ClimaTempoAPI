using ClimaLocal.Client.Interfaces;
using System.Text;

namespace ClimaLocal.Client;

public class RestClimaTempo : IRestClimaTempo
{
    private readonly HttpClient _restClimaTempo;

    public RestClimaTempo()
    {
        _restClimaTempo = new HttpClient();
    }

    public async Task<string> GetAsync(string url)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Content = new StringContent("", Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _restClimaTempo.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            return $"Erro na requisição GET: {e.Message}";
        }
    }
}


