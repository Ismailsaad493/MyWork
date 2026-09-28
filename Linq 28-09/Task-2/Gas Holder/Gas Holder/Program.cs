using Gas_Holder;

internal class Program
{
    static void Main(string[] args)
    {
        List<SensorReading> incomingData = new List<SensorReading>();

        incomingData.Add(new SensorReading("Tank_A", 85.5, true));
        incomingData.Add(new SensorReading("Tank_B", 120.2, true));
        incomingData.Add(new SensorReading("Tank_C", 150.0, false)); 
        incomingData.Add(new SensorReading("Tank_D", 105.8, true));

        List<SensorReading> criticalAlerts = incomingData
            .Where(s => s.IsOnline && s.Pressure > 100)
            .ToList();

        Console.WriteLine("--- Active Critical Pressure Alerts ---");

        foreach (SensorReading alert in criticalAlerts)
        {
            Console.WriteLine($"ALERT: Sensor {alert.SensorId} is reading critical pressure at {alert.Pressure}!");
        }

        Console.ReadLine();
    }
}
