using System;

namespace RealEstate.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public int Rating { get; set; }   // từ 1 đến 5
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}