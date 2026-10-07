using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadcast_Messaging_System
{
    public class CampaignMessage : IBroadcastable
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public MessageStatus Status { get; set; } = MessageStatus.Pending;
        public DateTime? DeliveredAt { get; set; }

        public void Send(Contact recipient)
        {
            Status = MessageStatus.Delivered;
            DeliveredAt = DateTime.Now;
        }
    }
    
    
}
