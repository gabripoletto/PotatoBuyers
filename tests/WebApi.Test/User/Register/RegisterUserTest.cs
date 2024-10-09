using CommomTestUtilities.Requests;
using FluentAssertions;
using PotatoBuyers.Communication.Requests;
using PotatoBuyers.Exceptions;
using PotatoBuyers.Exceptions.ResponsesMessages;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;
using Xunit;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly string method = "user";

        private readonly HttpClient _httpClient;

        public RegisterUserTest(CustomWebApplicationFactory factory) 
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            var request = RequestRegisterUserJsonBuilder.Build();

            var response = await _httpClient.PostAsJsonAsync(method, request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("response").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Empty_Name(string culture)
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            request.Name = string.Empty;

            if (_httpClient.DefaultRequestHeaders.Contains("Accept-language"))
                _httpClient.DefaultRequestHeaders.Remove("Accept-language");

            _httpClient.DefaultRequestHeaders.Add("Accept-language", culture);

            var response = await _httpClient.PostAsJsonAsync(method, request);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            //Caso for usar ResourceMessagesException com mensagens em varios idiomas
            //var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));
            var expectedMessage = ErrorMessages.REQUIRED_FIELD;

            errors.Should().ContainSingle().And.Contain(errors => errors.GetString()!.Equals(expectedMessage));
        }
    }
}
