using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            PostViews = new HashSet<PostView>();
            PostVotes = new HashSet<PostVote>();
        }

        public int PostId { get; set; }
        public int SenderId { get; set; }
        public int? GroupId { get; set; }
        public int? ProfileId { get; set; }
        public string? Content { get; set; }
        public DateTime DataTime { get; set; }
        public string? Messagescol { get; set; }

        public virtual Group? Group { get; set; }
        public virtual User Sender { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PostView> PostViews { get; set; }
        public virtual ICollection<PostVote> PostVotes { get; set; }
    }
}
