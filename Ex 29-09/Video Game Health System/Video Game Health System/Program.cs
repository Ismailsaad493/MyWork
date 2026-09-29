using Video_Game_Health_System;

internal class Program
{
    static void Main(string[] args)
    {
        Player myPlayer = new Player("Saad", 100);

        myPlayer.OnDeath += () =>
        {
            Console.WriteLine("\n[GAME MANAGER]: GAME OVER! Please insert coin to restart.");
        };

        Console.WriteLine("--- Battle Starts ---");

        myPlayer.TakeDamage(40);
        myPlayer.TakeDamage(70);
        Console.ReadLine();
    }

}