using System.Net.Http.Json;
using Xunit;

namespace WebApi.Test
{
    public class PotatoBuyersClassFixture : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public PotatoBuyersClassFixture(CustomWebApplicationFactory factory) 
        {
            _httpClient = factory.CreateClient();
        }

        protected async Task<HttpResponseMessage> DoPost(string method, object request, string culture = "pt-BR")
        {
            ChangeRequestCulture(culture);

            return await _httpClient.PostAsJsonAsync(method, request);
        }

        private void ChangeRequestCulture(string culture)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Accept-language"))
                _httpClient.DefaultRequestHeaders.Remove("Accept-language");

            _httpClient.DefaultRequestHeaders.Add("Accept-language", culture);
        }
    }
}

