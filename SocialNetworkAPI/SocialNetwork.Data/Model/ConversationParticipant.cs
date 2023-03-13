using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class ConversationParticipant
    {
        public ConversationParticipant()
        {
            Messages = new HashSet<Message>();
        }

        public int ParticipantId { get; set; }
        public int UserId { get; set; }
        public int ConversationId { get; set; }
        public DateTime? LastViewDate { get; set; }

        public virtual User Conversation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Message> Messages { get; set; }
    }
}
