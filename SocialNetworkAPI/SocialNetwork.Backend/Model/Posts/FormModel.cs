namespace SocialNetwork.Backend.Model.Posts
{
    public class FormModel
    {
        public long ProfileId { get; set; }
        public long? GroupId { get; set; }
        public string? Content { get; set; }
    }
}
