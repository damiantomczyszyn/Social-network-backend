using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class Group
    {
        public Group()
        {
            Posts = new HashSet<Post>();
        }

        public int GroupId { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public sbyte? IsClosed { get; set; }
        public int? ConversationId { get; set; }

        public virtual User Creator { get; set; } = null!;
        public virtual ICollection<Post> Posts { get; set; }
    }
}
