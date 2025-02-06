using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Monitoring_System.Entities
{
    public class WeatherData
    {
        public string? Location { set; get; }
        public double Temperature { set; get; }
        public double Humidity { set; get; }

        public WeatherData(string location, double temperature, double humidity)
        {
            Location = location;
            Temperature = temperature;
            Humidity = humidity;
        }
    }
}
