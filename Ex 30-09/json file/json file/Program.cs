using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class Contact
{
    public string Name { get; set; }
    public string Phone { get; set; }
}

internal class Program
{
    static void Main(string[] args)
    {
        string filePath = "contacts.json";
        List<Contact> myBook = new List<Contact>();

        Console.WriteLine("---CONTACT BOOK---");

        if (File.Exists(filePath))
        {
            string jsonText = File.ReadAllText(filePath);

            myBook = JsonSerializer.Deserialize<List<Contact>>(jsonText) ?? new List<Contact>();

            Console.WriteLine("Loaded " + myBook.Count + " contacts from previous session:");

            foreach (var contact in myBook)
            {
                Console.WriteLine($" - {contact.Name} ({contact.Phone})");
            }
        }
        else
        {
            Console.WriteLine("No save file found. Starting a fresh contact book.");
        }

        Console.WriteLine("\nAdding 'Ismail' to the contact book...");
        myBook.Add(new Contact { Name = "Ismail", Phone = "555-1234" });

        string newJsonText = JsonSerializer.Serialize(myBook, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(filePath, newJsonText);

        Console.WriteLine("\nContact saved! Run the program again to see it load automatically.");
        Console.ReadLine();
    }
}