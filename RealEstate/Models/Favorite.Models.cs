using System;

namespace RealEstate.Models
{
    public class Favorite
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}