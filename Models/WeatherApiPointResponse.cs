using System.Text.Json.Serialization;

public class WeatherApiPointResponse
{
    public string Id { get; set; }
    public string Type { get; set; }
    public FeatureProperties Properties { get; set; }
}

public class FeatureProperties
{
    [JsonPropertyName("@id")]
    public string FeatureId { get; set; }
    [JsonPropertyName("@type")]
    public string FeatureType { get; set; }
    public string Cwa { get; set; }
    public string ForecastOffice { get; set; }
    public string GridId { get; set; }
    public int GridX { get; set; }
    public int GridY { get; set; }
    public string Forecast { get; set; }
    public string ForecastHourly { get; set; }
    public string ForecastGridData { get; set; }
    public string ObservationStations { get; set; }
    public RelativeLocation RelativeLocation { get; set; }
    public string ForecastZone { get; set; }
    public string County { get; set; }
    public string FireWeatherZone { get; set; }
    public string TimeZone { get; set; }
    public string RadarStation { get; set; }
}

public class RelativeLocation
{
    public PointGeometry Geometry { get; set; }
    public RelativeLocationProperties Properties { get; set; }
}

public class PointGeometry
{
    public string Type { get; set; }
    public List<double> Coordinates { get; set; }
}

public class RelativeLocationProperties
{
    public string City { get; set; }
    public string State { get; set; }
    public Distance Distance { get; set; }
    public Bearing Bearing { get; set; }
}

public class Distance
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; }
    public double Value { get; set; }
}

public class Bearing
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; }
    public int Value { get; set; }
}