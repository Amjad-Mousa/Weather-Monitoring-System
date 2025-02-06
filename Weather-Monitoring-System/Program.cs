using System;
using Weather_Monitoring_System.Entities;
using Weather_Monitoring_System.Bots;

namespace Weather_Monitoring_System
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Weather Monitoring System");
            Console.WriteLine("1. Load weather data from JSON");
            Console.WriteLine("2. Load weather data from XML");
            Console.WriteLine("3. Show bot status");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            var option = Console.ReadLine();

            WeatherData weatherData = null;
            BotConfigContainer botConfigContainer = null;

            switch (option)
            {
                case "1":
                    Console.Write("Enter path to configuration JSON file: ");
                    string jsonConfigFilePath = Console.ReadLine();
                    try
                    {
                        botConfigContainer = JsonWeatherDataParser.ReadBotConfigFromFile(jsonConfigFilePath);
                        Console.Write("Enter path to JSON weather data file: ");
                        string jsonFilePath = Console.ReadLine();
                        weatherData = JsonWeatherDataParser.ReadWeatherDataFromFile(jsonFilePath);
                        Console.WriteLine("Weather data loaded from JSON successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "2":
                    Console.Write("Enter path to configuration JSON file: ");
                    string xmlConfigFilePath = Console.ReadLine();
                    try
                    {
                        botConfigContainer = JsonWeatherDataParser.ReadBotConfigFromFile(xmlConfigFilePath);
                        Console.Write("Enter path to XML weather data file: ");
                        string xmlFilePath = Console.ReadLine();
                        weatherData = XmlWeatherDataParser.ReadFromFile(xmlFilePath);
                        Console.WriteLine("Weather data loaded from XML successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "3":
                    if (weatherData == null || botConfigContainer == null)
                    {
                        Console.WriteLine("Please load weather data first.");
                    }
                    else
                    {
                        Console.WriteLine("Bot Status:");

                        if (botConfigContainer.RainBot != null && botConfigContainer.RainBot.Enabled)
                        {
                            Console.WriteLine("RainBot is enabled.");
                            Console.WriteLine($"Humidity threshold: {botConfigContainer.RainBot.HumidityThreshold}");
                            Console.WriteLine($"Message: {botConfigContainer.RainBot.Message}");
                            Console.WriteLine($"Current Weather: {weatherData.Location}, Temperature: {weatherData.Temperature}, Humidity: {weatherData.Humidity}");

                            if (weatherData.Humidity > botConfigContainer.RainBot.HumidityThreshold)
                            {
                                var rainBot = new RainBot(botConfigContainer.RainBot.HumidityThreshold, botConfigContainer.RainBot.Message);
                                rainBot.Activate(weatherData);
                            }
                        }

                        if (botConfigContainer.SunBot != null && botConfigContainer.SunBot.Enabled)
                        {
                            Console.WriteLine("SunBot is enabled.");
                            Console.WriteLine($"Temperature threshold: {botConfigContainer.SunBot.TemperatureThreshold}");
                            Console.WriteLine($"Message: {botConfigContainer.SunBot.Message}");

                            if (weatherData.Temperature > botConfigContainer.SunBot.TemperatureThreshold)
                            {
                                var sunBot = new SunBot(botConfigContainer.SunBot.TemperatureThreshold, botConfigContainer.SunBot.Message);
                                sunBot.Activate(weatherData);
                            }
                        }

                        if (botConfigContainer.SnowBot != null && botConfigContainer.SnowBot.Enabled)
                        {
                            Console.WriteLine("SnowBot is enabled.");
                            Console.WriteLine($"Temperature threshold: {botConfigContainer.SnowBot.TemperatureThreshold}");
                            Console.WriteLine($"Message: {botConfigContainer.SnowBot.Message}");

                            if (weatherData.Temperature < botConfigContainer.SnowBot.TemperatureThreshold)
                            {
                                var snowBot = new SnowBot(botConfigContainer.SnowBot.TemperatureThreshold, botConfigContainer.SnowBot.Message);
                                snowBot.Activate(weatherData);
                            }
                        }
                    }
                    break;

                case "4":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Main(args);
        }
    }
}
