using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Monitoring_System.Entities
{
    public class BotConfigContainer
    {
        public BotConfig? RainBot { get; set; }
        public BotConfig? SunBot { get; set; }
        public BotConfig? SnowBot { get; set; }
    }
}
