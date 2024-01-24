using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IGeocodingService
    {
        Task<Location> GetLocation(string address);
    }
}