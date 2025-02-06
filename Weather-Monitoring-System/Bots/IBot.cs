using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Monitoring_System.Bots
{
    public interface IBot
    {
        public void Activate(WeatherData data);
    }
}
