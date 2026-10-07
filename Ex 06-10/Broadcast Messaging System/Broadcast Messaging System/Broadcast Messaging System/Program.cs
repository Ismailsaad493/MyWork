using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadcast_Messaging_System
{
    internal class program
    {

        static async Task Main(string[] args)
        {
            BroadcastManager manager = new BroadcastManager();
            manager.LoadLog();

            if (!manager.Messages.Any())
            {
                manager.Messages.Add(new CampaignMessage { Id = 1, Content = "Promo: 50% off!" });
                manager.Messages.Add(new CampaignMessage { Id = 2, Content = "Reminder: Webinar at 3PM" });
                manager.Messages.Add(new CampaignMessage { Id = 3, Content = "Alert: Account login detected" });
                manager.SaveLog();
            }

            Contact testUser = new Contact { Id = 101, Name = "TestUser", PhoneNumber = "+919876543210" };
            manager.OnDeliverySuccess += (message) =>
            {
                Console.WriteLine($"[SUCCESS] Message '{message.Id}' delivered at {message.DeliveredAt}");
            };

            Console.WriteLine("Starting broadcast transmission...\n");

            var pendingMessages = manager.GetPendingMessages();
            foreach (var msg in pendingMessages)
            {
                await manager.TransmitMessageAsync(msg.Id, testUser);
            }

            Console.WriteLine("\nBroadcast complete. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}