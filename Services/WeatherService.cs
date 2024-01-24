// WeatherApp/Services/WeatherService.cs
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> GetWeatherForecast(double latitude, double longitude)
        {
            try
            {
                // Construct the API endpoint URL
                string apiUrl = $"https://api.weather.gov/points/{latitude},{longitude}";

                _httpClient.DefaultRequestHeaders.Add("User-Agent", "MyWeatherApp (contact@myweatherapp.com)"); 

                // Make a request to the API endpoint
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                // Read the response content
                string jsonContent = await response.Content.ReadAsStringAsync();
                string cleanedJson = jsonContent
            .Replace("\n", "")
            .Replace("\r", "")
            .Replace("\\", "")
            .Replace(" ", "");

        // Deserialize the cleaned JSON string
            var weatherApiResponse = JsonSerializer.Deserialize<WeatherApiPointResponse>(cleanedJson, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (weatherApiResponse?.Properties?.Forecast == null)
        {
            throw new Exception("Forecast URL not found in the response.");
        }
                // Make a request to the forecast URL to get the actual 7-day forecast
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "MyWeatherApp (contact@myweatherapp.com)"); 
                HttpResponseMessage forecastResponse = await _httpClient.GetAsync(weatherApiResponse.Properties.Forecast);
                forecastResponse.EnsureSuccessStatusCode();

                // Read the forecast response content
                string forecastJsonContent = await forecastResponse.Content.ReadAsStringAsync();
                string cleanedJsonForecast = forecastJsonContent
                    .Replace("\n", "")
                    .Replace("\r", "")
                    .Replace("\\", "")
                    .Replace(" ", "");

                return forecastJsonContent;
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors (e.g., network issues)
                // You may want to log the error or throw a custom exception
                throw new Exception("Error while contacting the Weather service.", ex);
            }
        }
    }

    // Define classes to represent the JSON response structure
  

}
