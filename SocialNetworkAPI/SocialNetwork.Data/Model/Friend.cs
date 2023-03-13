using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class Friend
    {
        public int User1Id { get; set; }
        public int User2Id { get; set; }
        public DateOnly? FriendshipStartDate { get; set; }

        public virtual User User1 { get; set; } = null!;
        public virtual User User2 { get; set; } = null!;
    }
}
