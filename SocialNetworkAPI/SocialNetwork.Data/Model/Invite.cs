using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class Invite
    {
        public int InvitingUserId { get; set; }
        public int InvitedUserId { get; set; }
        public DateOnly? InviteDate { get; set; }

        public virtual User InvitedUser { get; set; } = null!;
        public virtual User InvitingUser { get; set; } = null!;
    }
}
