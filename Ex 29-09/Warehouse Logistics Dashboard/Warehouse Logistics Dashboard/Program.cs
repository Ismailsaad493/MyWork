using Warehouse_Logistics_Dashboard;
class Program
{
    static void Main(string[]args)
    {
        List<Shipment> cargoList = new List<Shipment>();

        cargoList.Add(new Shipment(101, "Mumbai", 45.5, true));
        cargoList.Add(new Shipment(102, "Delhi", 12.0, false));
        cargoList.Add(new Shipment(103, "Mumbai", 85.0, false));
        cargoList.Add(new Shipment(104, "Chennai", 5.5, true));
        cargoList.Add(new Shipment(105, "Delhi", 20.0, true));

        Console.WriteLine("--- Safety List---");
        bool hasFragileItemss = cargoList.Any(s => s.IsFragile == true);
        Console.WriteLine("Contains Fragile Items: " + hasFragileItemss);

        Console.WriteLine("--- Element Operator ---");
        Shipment urgentBox = cargoList.First(s => s.TrackingId == 104);
        Console.WriteLine("CIty name is {City} and Weight is {Weight} ");

        Console.WriteLine("--- --- --- ---");

        List<int> mumbaiTrackingIds = cargoList
            .Where(s => s.City == "Mumbai")
            .OrderByDescending(s => s.Weight)
            .Select(s => s.TrackingId)
            .ToList();

        foreach(Shipment datas in cargoList) 
        {
            Console.WriteLine("Tracking Ids: " +  datas.TrackingId);
        }

        Console.WriteLine("\n--- Shipping Manifest by City ---");
        var groupedCargo = cargoList.GroupBy(s => s.City);

        foreach(var group in groupedCargo)
        {
            Console.WriteLine($"City: {group.Key} | Total Shipments: {group.Count()}");
        }
    }
}