//using SocialNetwork.Backend.Model.User;
using System;


namespace SocialNetwork.Backend.Model.Groups
{
    public class FormModel
    {
        public int GroupId { get; set; }
        public int CreatorId { get; set; }
        public sbyte? IsClosed { get; set; }

        //Users list accounts
    }
}
