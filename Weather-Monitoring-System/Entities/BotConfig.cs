namespace Weather_Monitoring_System.Entities
{
    public class BotConfig
    {
        public bool Enabled { get; set; }
        public double TemperatureThreshold { get; set; }
        public double HumidityThreshold { get; set; }
        public string? Message { get; set; }
    }
}
