namespace Weather_Monitoring_System
{
    public class XmlWeatherDataParser
    {
        public static WeatherData Parse(string xml)
        {
            var doc = new System.Xml.XmlDocument();
            if (string.IsNullOrEmpty(xml))
            {
                throw new System.ArgumentException("XML string cannot be null or empty");
            }
            doc.LoadXml(xml);

            var locationNode = doc.GetElementsByTagName("Location").Cast<System.Xml.XmlNode>().FirstOrDefault();
            if (locationNode == null)
            {
                throw new System.ArgumentException("XML string must contain a Location element");
            }
            var location = locationNode.InnerText;

            var temperatureNode = doc.GetElementsByTagName("Temperature").Cast<System.Xml.XmlNode>().FirstOrDefault();
            if (temperatureNode == null)
            {
                throw new System.ArgumentException("XML string must contain a Temperature element");
            }
            var temperature = double.Parse(temperatureNode.InnerText);

            var humidityNode = doc.GetElementsByTagName("Humidity").Cast<System.Xml.XmlNode>().FirstOrDefault();
            if (humidityNode == null)
            {
                throw new System.ArgumentException("XML string must contain a Humidity element");
            }
            var humidity = double.Parse(humidityNode.InnerText);

            return new WeatherData(location, temperature, humidity);
        }

        public static WeatherData ReadFromFile(string filePath)
        {
            string xml = System.IO.File.ReadAllText(filePath);
            return XmlWeatherDataParser.Parse(xml);
        }
    }
}
