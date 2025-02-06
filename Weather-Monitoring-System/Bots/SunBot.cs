using Weather_Monitoring_System.Entities;

namespace Weather_Monitoring_System.Bots
{
    public class SunBot : IBot
    {
        private double _temperatureThreshold;
        private string _message;

        public SunBot(double temperatureThreshold, string message)
        {
            _temperatureThreshold = temperatureThreshold;
            _message = message;
        }

        public void Activate(WeatherData data)
        {
            if (data.Temperature > _temperatureThreshold)
            {
                Console.WriteLine(_message);
            }
        }
    }
}
