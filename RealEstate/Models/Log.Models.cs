using System;

namespace RealEstate.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public string Target { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}