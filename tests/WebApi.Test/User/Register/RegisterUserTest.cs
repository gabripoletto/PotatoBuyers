﻿using CommomTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public RegisterUserTest(WebApplicationFactory<Program> factory) 
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task Success()
        {
            var request = RegisterUserValidatorBuilder.Build();

            var response = await _httpClient.PostAsJsonAsync("User", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            responseData.RootElement.GetProperty("response").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
        }
    }
}
                                       