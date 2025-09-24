using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.DTO
{
    public class PropertyDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public int TypeId { get; set; }
        public string Status { get; set; }
    }
}