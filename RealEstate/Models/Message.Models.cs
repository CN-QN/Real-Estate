using System;

namespace RealEstate.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int? PropertyId { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }  // Sent, Read, Deleted...
        public DateTime CreatedAt { get; set; }
    }
}