using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Weather_Monitoring_System.Bots;
using Weather_Monitoring_System.Entities;

class Program
{
    static void Main()
    {
        Console.Write("Enter path to configuration JSON file: ");
        string configPath = Console.ReadLine();

        if (!File.Exists(configPath))
        {
            Console.WriteLine("Configuration file not found.");
            return;
        }

        string configJson = File.ReadAllText(configPath);
        var configData = JsonConvert.DeserializeObject<Dictionary<string, BotConfig>>(configJson);
        var bots = new List<IBot>();

        if (configData.ContainsKey("RainBot") && configData["RainBot"].Enabled)
            bots.Add(new RainBot(configData["RainBot"].HumidityThreshold, configData["RainBot"].Message));

        if (configData.ContainsKey("SunBot") && configData["SunBot"].Enabled)
            bots.Add(new SunBot(configData["SunBot"].TemperatureThreshold, configData["SunBot"].Message));

        if (configData.ContainsKey("SnowBot") && configData["SnowBot"].Enabled)
            bots.Add(new SnowBot(configData["SnowBot"].TemperatureThreshold, configData["SnowBot"].Message));

        Console.WriteLine("Configuration file loaded successfully.");

        WeatherData weatherData = null;

        while (true)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Load weather data from JSON");
            Console.WriteLine("2. Load weather data from XML");
            Console.WriteLine("3. Show bot status");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    weatherData = LoadWeatherDataFromJson();
                    break;
                case "2":
                    weatherData = LoadWeatherDataFromXml();
                    break;
                case "3":
                    if (weatherData == null)
                    {
                        Console.WriteLine("No weather data loaded yet.");
                    }
                    else
                    {
                        Console.WriteLine("\nBot Status:");
                        foreach (var bot in bots)
                        {
                            bot.Activate(weatherData);
                        }
                    }
                    break;
                case "4":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static WeatherData LoadWeatherDataFromJson()
    {
        Console.Write("Enter path to JSON weather data file: ");
        string filePath = Console.ReadLine();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Weather data file not found.");
            return null;
        }

        string json = File.ReadAllText(filePath);
        var weatherData = JsonConvert.DeserializeObject<WeatherData>(json);
        Console.WriteLine("Weather data loaded from JSON successfully.");
        return weatherData;
    }

    static WeatherData LoadWeatherDataFromXml()
    {
        Console.Write("Enter path to XML weather data file: ");
        string filePath = Console.ReadLine();

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Weather data file not found.");
            return null;
        }

        System.Xml.Serialization.XmlSerializer serializer = new(typeof(WeatherData));
        using FileStream stream = new(filePath, FileMode.Open);
        var weatherData = (WeatherData)serializer.Deserialize(stream);
        Console.WriteLine("Weather data loaded from XML successfully.");
        return weatherData;
    }
}
