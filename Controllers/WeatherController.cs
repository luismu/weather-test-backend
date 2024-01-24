using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly GeocodingService _geocodingService;
        private readonly WeatherService _weatherService;

        public WeatherController(GeocodingService geocodingService, WeatherService weatherService)
        {
            _geocodingService = geocodingService;
            _weatherService = weatherService;
        }

        [HttpGet("forecast")]
        public async Task<IActionResult> GetForecast(string address)
        {
            try
            {
                // Use geocoding service to get latitude and longitude
                var location = await _geocodingService.GetLocation(address);

                // Use the obtained location to get weather forecast
                // (Implementation for weather service is to be added in the next step)
                var weatherForecast = await _weatherService.GetWeatherForecast(location.Latitude, location.Longitude);

                return Ok(weatherForecast);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
