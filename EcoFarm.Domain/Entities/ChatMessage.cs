using EcoFarm.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Domain.Entities
{
    public class ChatMessage : BaseNonExtendedEntity
    {
        public string ChatId { get; set; }
        public string SenderAccountId { get; set; }
        public string ReceiverAccountId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; } = false;

    }
}
