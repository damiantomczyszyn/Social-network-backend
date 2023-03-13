using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class Comment
    {
        public Comment()
        {
            CommentVotes = new HashSet<CommentVote>();
        }

        public int CommentId { get; set; }
        public int SenderId { get; set; }
        public int PostId { get; set; }
        public string? Content { get; set; }
        public DateTime DateTime { get; set; }
        public sbyte? WasRemoved { get; set; }

        public virtual Post Post { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
        public virtual ICollection<CommentVote> CommentVotes { get; set; }
    }
}
