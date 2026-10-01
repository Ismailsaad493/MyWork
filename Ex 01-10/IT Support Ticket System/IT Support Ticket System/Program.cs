using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ITSupportSystem
{
    public enum TicketStatus
    {
        Pending,
        Resolved
    }

    public interface ITicket
    {
        void PrintDetails();
    }

    public class SupportTicket : ITicket
    {
        public int Id { get; set; }
        public string Issue { get; set; }
        public TicketStatus Status { get; set; }
        public string? ResolutionNotes { get; set; }

        public void PrintDetails()
        {
            Console.WriteLine($"Ticket #{Id}: {Issue} - [{Status}]");
        }
    }

    public class LocalDatabase<T>
    {
        public void SaveData(List<T> items, string fileName)
        {
            try
            {
                string json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(fileName, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
            }
        }
    }

    public class TicketResolver
    {
        public event Action<SupportTicket> OnTicketResolved;

        public async Task ResolveTicketAsync(SupportTicket ticket)
        {
            Console.WriteLine($"Working on ticket: {ticket.Issue}");
            await Task.Delay(2000);
            ticket.Status = TicketStatus.Resolved;
            ticket.ResolutionNotes = "Rebooted the server.";
            OnTicketResolved?.Invoke(ticket);
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            List<SupportTicket> tickets = new List<SupportTicket>
            {
                new SupportTicket { Id = 1, Issue = "Wi-Fi is down", Status = TicketStatus.Pending},
                new SupportTicket { Id = 2, Issue = "Blue screen of death", Status = TicketStatus.Pending},
                new SupportTicket { Id = 3, Issue = "Forget password", Status = TicketStatus.Pending},
            };

            var pendingTickets = tickets.Where(t => t.Status == TicketStatus.Pending).ToList();

            TicketResolver resolver = new TicketResolver();

            resolver.OnTicketResolved += (ticket) =>
            {
                Console.WriteLine($"[Alert] Ticket fixed: {ticket.Id}");
                ticket.PrintDetails();
            };

            foreach (var ticket in pendingTickets)
            {
                await resolver.ResolveTicketAsync(ticket);
            }

            LocalDatabase<SupportTicket> db = new LocalDatabase<SupportTicket>();
            db.SaveData(tickets, "tickets.json");

            Console.WriteLine("\nAll tasks complete. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}