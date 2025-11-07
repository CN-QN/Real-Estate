using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class PostViewModels
    {
        public string TenDuAn { get; set; }
        public int LoaiBDS { get; set; } // Giả định là ID của Loại BĐS
        public decimal GiaMin { get; set; }
        public decimal GiaMax { get; set; }
        public decimal DienTichMin { get; set; }
        public decimal DienTichMax { get; set; }
        public string MoTa { get; set; }

        // Địa chỉ
        public int ProvinceCode { get; set; }
        public int DistrictCode { get; set; }
        public int? WardCode { get; set; } // Có thể null
        public string StreetName { get; set; }
        public string AddressDetail { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        // Gói tin và thời gian (chỉ lấy loại tin, thời gian và ngày)
        public string LoaiTin { get; set; }
        public int ThoiGianTuan { get; set; }
        public DateTime NgayBatDau { get; set; }

        // Danh sách URL ảnh từ Cloudinary (Rất quan trọng)
        public List<string> ImageUrls { get; set; }
    }
}