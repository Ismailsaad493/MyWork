using System;

namespace MovieTheater
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] movies = new string[3];
            movies[0] = "Inception";
            movies[1] = "War";
            movies[2] = "Interstellar";

            try
            {
                Console.Write("Enter a screen number (0, 1, or 2) to see what is playing: ");
                int screenNumber = int.Parse(Console.ReadLine());

                Console.WriteLine("You are watching: " + movies[screenNumber]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: That screen does not exist! Please pick 0, 1, or 2.");
            }

            Console.ReadLine();
        }
    }
}