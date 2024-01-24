// WeatherApp/Services/IWeatherService.cs
namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<string> GetWeatherForecast(double latitude, double longitude);
    }
}
