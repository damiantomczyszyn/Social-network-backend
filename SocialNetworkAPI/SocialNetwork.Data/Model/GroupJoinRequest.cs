using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class GroupJoinRequest
    {
        public int RequestingUserId { get; set; }
        public int GroupId { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual User RequestingUser { get; set; } = null!;
    }
}
