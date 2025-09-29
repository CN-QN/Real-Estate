using System;

namespace RealEstate.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}