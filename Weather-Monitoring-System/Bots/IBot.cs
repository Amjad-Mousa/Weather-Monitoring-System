using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_Monitoring_System.Entities;

namespace Weather_Monitoring_System.Bots
{
    public interface IBot
    {
        public void Activate(WeatherData data);
    }
}
