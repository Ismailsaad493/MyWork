using System.Linq.Expressions;
using Video_Game_Enemy_Spawner;

try
{
    Console.WriteLine("Enter the current game wave number:");
    int waveNumber = int.Parse(Console.ReadLine());

    List<Enemy> spawnedEnemies = new List<Enemy>();
    spawnedEnemies.Add(new Zombie("Walker", 15));
    spawnedEnemies.Add(new Robot("Mech-X", 40));

    Console.WriteLine("mob's Name" + waveNumber + "Strated!---");


    foreach (Enemy mob in spawnedEnemies)
    {
        Console.WriteLine("Enemy: " + mob.Name + "Damage:" + mob.Damage);
        mob.Attack();
        Console.WriteLine("--------------");
    }
}
    
    catch (FormatException)
        {
    Console.WriteLine("Error: Please type a valid number for the game wave!");

}


Console.ReadLine();

