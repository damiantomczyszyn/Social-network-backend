using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class GroupParticipant
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public sbyte? IsAdmin { get; set; }
        public string? Nick { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
