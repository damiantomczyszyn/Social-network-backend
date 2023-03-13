using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class PostVote
    {
        public int PostVoteId { get; set; }
        public int PostId { get; set; }
        public int VotingUserId { get; set; }
        public sbyte? IsUp { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual User VotingUser { get; set; } = null!;
    }
}
