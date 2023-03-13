using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class BlockedUser
    {
        public int BlockingUserId { get; set; }
        public int BlockedUserId { get; set; }
        public DateOnly? BlockedDate { get; set; }

        public virtual User BlockedUserNavigation { get; set; } = null!;
        public virtual User BlockingUser { get; set; } = null!;
    }
}
