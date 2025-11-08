using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class PostViewModels
    {
        [Required(ErrorMessage = " Vui lòng nhập tiêu đề bài đăng")]
        public string TenDuAn { get; set; }
        [Required(ErrorMessage = " Vui lòng chọn loại bất động sản")]
        public int LoaiBDS { get; set; } // Giả định là ID của Loại BĐS
        [Required(ErrorMessage = " Vui lòng nhập giá min")]
        public decimal GiaMin { get; set; }
        [Required(ErrorMessage = " Vui lòng nhập giá max")]

        public decimal GiaMax { get; set; }
        [Required(ErrorMessage = " Vui lòng nhập diện tích min")]
        public decimal DienTichMin { get; set; }
        [Required(ErrorMessage = " Vui lòng nhập diện tích max")]
        public decimal DienTichMax { get; set; }
        [Required(ErrorMessage = " Vui lòng nhập mô tả")]
        public string MoTa { get; set; }

        // Địa chỉ
        [Required(ErrorMessage = " Vui lòng chọn tỉnh/thành phố")]
        public int ProvinceCode { get; set; }
        [Required(ErrorMessage = " Vui lòng chọn quận/huyện")]
        public int DistrictCode { get; set; }
        [Required(ErrorMessage = " Vui lòng chọn phường/xã")]

        public string WardCode { get; set; } // Có thể null

        [Required(ErrorMessage = " Vui lòng nhập tên đường")]
        public string StreetName { get; set; }
        [Required(ErrorMessage = " Vui lòng nhập địa chỉ chi tiết")]
        public string AddressDetail { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public List<string> ImageUrls { get; set; }
    }
}