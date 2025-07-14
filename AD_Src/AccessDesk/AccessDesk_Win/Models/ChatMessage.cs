using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDesk_Win.Models
{
    public class ChatMessage
    {
        public string User { get; set; }
        public string Message { get; set; }

        public override string ToString() => $"{User}: {Message}";
    }
}
