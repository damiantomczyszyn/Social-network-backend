using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class CommentVote
    {
        public int CommentVoteId { get; set; }
        public int CommentId { get; set; }
        public int VotingUserId { get; set; }
        public sbyte? IsUp { get; set; }

        public virtual Comment Comment { get; set; } = null!;
        public virtual User VotingUser { get; set; } = null!;
    }
}
