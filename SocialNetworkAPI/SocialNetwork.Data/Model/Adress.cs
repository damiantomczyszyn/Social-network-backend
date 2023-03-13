using System;
using System.Collections.Generic;

namespace SocialNetwork.Data.Model
{
    public partial class Adress
    {
        public int UsersUserId { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }

        public virtual User UsersUser { get; set; } = null!;
    }
}
