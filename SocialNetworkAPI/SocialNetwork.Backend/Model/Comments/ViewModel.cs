namespace SocialNetwork.Backend.Model.Comments
{
    public class ViewModel
    {
        public int CommentId { get; set; }
        public int SenderId { get; set; }
        public int PostId { get; set; }
        public string? Content { get; set; }
        public string? CommentatorName { get; set; } //null narazie 
        public int CommentsCount { get; set; }
        public int VotesSum { get; set; }
        public DateTime DateTime { get; set; }
    }
}
