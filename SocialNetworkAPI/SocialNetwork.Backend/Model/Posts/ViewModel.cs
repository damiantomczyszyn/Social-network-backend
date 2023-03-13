namespace SocialNetwork.Backend.Model.Posts
{
    public class ViewModel
    {
        public int PostId { get; set; }
        public int SenderId { get; set; }

        public string? Content { get; set; }
        public int CommentsCount { get; set; }
        public int VotesSum { get; set; }
    }
}
