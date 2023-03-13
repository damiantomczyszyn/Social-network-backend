namespace SocialNetwork.Backend.Model.Users
{
    public class ViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string? TelephoneNumber { get; set; }
        public DateOnly JoinDate { get; set; }
        public DateOnly BirthdayDate { get; set; }
        public sbyte? AllowInvites { get; set; }
        public string? WorkingPlace { get; set; }
        public string? LearningPlace { get; set; }
    }
}
