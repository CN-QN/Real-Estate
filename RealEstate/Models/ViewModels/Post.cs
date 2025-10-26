using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [MaxLength(200)]
        public string ProjectName { get; set; }

        [MaxLength(50)]
        public string TransactionType { get; set; } 

        [MaxLength(50)]
        public string PropertyType { get; set; } 

        [Required, MaxLength(250)]
        public string Title { get; set; }

        public string Description { get; set; } 

        public int? ProvinceCode { get; set; }
        public int? DistrictCode { get; set; }
        public int? WardCode { get; set; }

        [MaxLength(200)]
        public string StreetName { get; set; }

        [MaxLength(300)]
        public string AddressText { get; set; }

        [MaxLength(50)]
        public string Direction { get; set; }

        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public decimal? AreaMin { get; set; }
        public decimal? AreaMax { get; set; }

        [MaxLength(30)]
        public string Status { get; set; } 

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<PostImage> Images { get; set; } = new List<PostImage>();
    }
}