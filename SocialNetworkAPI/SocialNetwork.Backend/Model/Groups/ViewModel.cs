namespace SocialNetwork.Backend.Model.Groups
{
    public class ViewModel
    {
        public int GroupId { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        
        public int? ConversationId { get; set; }


    }
}
