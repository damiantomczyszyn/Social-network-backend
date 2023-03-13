using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class Conversation
    {
        public Conversation()
        {
            Messages = new HashSet<Message>();
        }

        public int ConversationId { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
