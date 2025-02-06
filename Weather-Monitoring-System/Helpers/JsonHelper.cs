namespace Weather_Monitoring_System
{
    public class JsonWeatherDataParser
    {
        public static WeatherData Parse(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentException("JSON string cannot be null or empty");
            }

            try
            {
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
                var location = data?.Location?.ToString();
                if (string.IsNullOrEmpty(location))
                {
                    throw new ArgumentException("JSON string must contain a valid Location element");
                }

                var temperature = data?.Temperature;
                if (temperature == null)
                {
                    throw new ArgumentException("JSON string must contain a valid Temperature element");
                }

                var humidity = data?.Humidity;
                if (humidity == null)
                {
                    throw new ArgumentException("JSON string must contain a valid Humidity element");
                }

                return new WeatherData(location, (double)temperature, (double)humidity);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Failed to parse JSON data", ex);
            }
        }

        public static WeatherData ReadFromFile(string filePath)
        {
            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found", filePath);
                }

                string json = System.IO.File.ReadAllText(filePath);
                return JsonWeatherDataParser.Parse(json);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Failed to read or parse the file", ex);
            }
        }
    }
}
