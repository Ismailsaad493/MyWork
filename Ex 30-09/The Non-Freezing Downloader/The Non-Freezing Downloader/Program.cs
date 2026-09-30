using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

internal class program
{
    static async Task Main(string[]args)
    {
        Console.WriteLine("Opening Weather App...");
        int currentTemp = await GetTemperatureAsync("Tokyo");
        Console.WriteLine("The current temperature is " + currentTemp + "°C");
        Console.ReadLine();

    }
    static async Task<int> GetTemperatureAsync(string city)
    {
        Console.WriteLine("Connecting ti weather satellite for" + city + "...");
        await Task.Delay(2000);
        return 28;
    }


}