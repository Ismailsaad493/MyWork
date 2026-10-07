using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broadcast_Messaging_System
{
    public partial interface IBroadcastable
    {
        void Send(Contact recipient);                  
        
    }
}
