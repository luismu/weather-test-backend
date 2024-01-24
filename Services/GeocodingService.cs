using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class GeocodingService
    {
        private readonly HttpClient _httpClient;

        public GeocodingService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Location> GetLocation(string address)
        { System.Diagnostics.Debug.WriteLine("address uri",Uri.EscapeDataString(address) );
            string geocodingApiUrl = 
$"https://geocoding.geo.census.gov/geocoder/locations/onelineaddress?address={Uri.EscapeDataString(address)}&benchmark=Public_AR_Current&format=json";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(geocodingApiUrl);
                response.EnsureSuccessStatusCode();
                string jsonContent = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("aqueueueurphfdfsbgahbf 123123", jsonContent);
                Location location = ParseGeocodingResponse(jsonContent);

                return location;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Error while contacting the Geocoding service.", ex);
            }
        }

        public class Location
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

    public Location ParseGeocodingResponse(string jsonContent)
    {
        var jsonDocument = JsonDocument.Parse(jsonContent);

        var addressMatchesArray = jsonDocument.RootElement
            .GetProperty("result")
            .GetProperty("addressMatches")
            .EnumerateArray()
            .FirstOrDefault();

        var coordinatesElement = addressMatchesArray
            .GetProperty("coordinates");

        double xValue = 0.0;
        double yValue = 0.0;

        if (coordinatesElement.TryGetProperty("x", out var x) == true)
        {
            xValue = x.GetDouble();
        }

        if (coordinatesElement.TryGetProperty("y", out var y) == true)
        {
            yValue = y.GetDouble();
        }

        return new Location { Latitude = yValue, Longitude = xValue };
    }
    }
}
