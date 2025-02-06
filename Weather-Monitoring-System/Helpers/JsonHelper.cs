using Weather_Monitoring_System.Entities;

namespace Weather_Monitoring_System
{
    public class JsonWeatherDataParser
    {
        public static WeatherData ParseWeatherData(string json)
        {
            ValidateJson(json);
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            var location = data?.Location?.ToString();
            var temperature = data?.Temperature;
            var humidity = data?.Humidity;

            ValidateWeatherData(location, temperature, humidity);

            return new WeatherData(location, (double)temperature, (double)humidity);
        }

        public static BotConfigContainer ParseBotConfig(string json)
        {
            ValidateJson(json);
            var config = Newtonsoft.Json.JsonConvert.DeserializeObject<BotConfigContainer>(json);

            return config;
        }

        public static WeatherData ReadWeatherDataFromFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("Weather data file not found", filePath);
            }

            string json = System.IO.File.ReadAllText(filePath);
            return ParseWeatherData(json);
        }

        public static BotConfigContainer ReadBotConfigFromFile(string configFilePath)
        {
            if (!System.IO.File.Exists(configFilePath))
            {
                throw new FileNotFoundException("Configuration file not found", configFilePath);
            }

            string configJson = System.IO.File.ReadAllText(configFilePath);
            return ParseBotConfig(configJson);
        }

        private static void ValidateJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new ArgumentException("JSON string cannot be null or empty");
            }
        }

        private static void ValidateWeatherData(string location, object temperature, object humidity)
        {
            if (string.IsNullOrEmpty(location))
            {
                throw new ArgumentException("JSON string must contain a valid Location element");
            }

            if (temperature == null)
            {
                throw new ArgumentException("JSON string must contain a valid Temperature element");
            }

            if (humidity == null)
            {
                throw new ArgumentException("JSON string must contain a valid Humidity element");
            }
        }
    }

 


}
