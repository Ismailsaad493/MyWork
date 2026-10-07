using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Broadcast_Messaging_System
{
    public class BroadcastManager
    {

       public List<CampaignMessage> Messages { get; set; } = new List<CampaignMessage>();
        private readonly string _filePath = "broadcast_log.json";
        public event Action<CampaignMessage> OnDeliverySuccess;

        public void SaveLog()
        {
            string json = JsonSerializer.Serialize(Messages, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        public void LoadLog()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                Messages = JsonSerializer.Deserialize<List<CampaignMessage>>(json) ?? new List<CampaignMessage>();
            }
        }

        public List<CampaignMessage> GetPendingMessages()
        {
            return Messages.Where(m => m.Status == MessageStatus.Pending).ToList();
        }
        public async Task TransmitMessageAsync(int messageId, Contact recipient)
        {
            await Task.Delay(2000);

            var message = Messages.FirstOrDefault(m => m.Id == messageId);
            if (message != null && message.Status == MessageStatus.Pending)
            {
                message.Send(recipient);
                SaveLog();
                OnDeliverySuccess?.Invoke(message);
            }
        }
    }
}
