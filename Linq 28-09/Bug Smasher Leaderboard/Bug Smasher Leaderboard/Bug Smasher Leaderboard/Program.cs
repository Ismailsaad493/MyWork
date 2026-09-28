using Bug_Smasher_Leaderboard;

internal class program
{
    static void Main(string[]args)
    {

        List<Developer> devTeam = new List<Developer>();
        devTeam.Add(new Developer("Saad", 45, true));
        devTeam.Add(new Developer("Saim", 82, true));
        devTeam.Add(new Developer("Anfal", 99, false));
        devTeam.Add(new Developer("Danish", 60, true));

        List<string> topPerformers = devTeam
        .Where(d => d.IsActiveEmployee == true)
        .OrderByDescending(d => d.BugsFixed)
        .Select(d => d.Name)
        .ToList();

        foreach(string name in topPerformers)
        {
            Console.WriteLine("Top Perfotmer: " + name); 
        }

        Console.ReadLine();
    }
}



