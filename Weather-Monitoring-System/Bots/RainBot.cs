using Weather_Monitoring_System.Entities;

namespace Weather_Monitoring_System.Bots
{
    public class RainBot : IBot
    {
        private double _humidityThreshold;
        private string _message;

        public RainBot(double humidityThreshold, string message)
        {
            _humidityThreshold = humidityThreshold;
            _message = message;
        }

        public void Activate(WeatherData data)
        {
            if (data.Humidity >= _humidityThreshold)
            {
                Console.WriteLine(_message);
            }
            else
            {
                Console.WriteLine($"No rain expected. Humidity ({data.Humidity}%) is below the threshold ({_humidityThreshold}%).");
            }
        }
    }
}
