using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class PostFormVM
    {
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại hình giao dịch.")]
        public string TransactionType { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại bất động sản.")]
        public string PropertyType { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Tỉnh/Thành phố.")]
        public string ProvinceCode { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn Quận/Huyện.")]
        public string DistrictCode { get; set; }
        public string WardCode { get; set; }
        public string StreetName { get; set; }
        public string AddressText { get; set; }
        public string Direction { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống.")]
        [MaxLength(99, ErrorMessage = "Tiêu đề không được vượt quá 99 ký tự.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá tối thiểu.")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Giá phải là số dương.")]
        public decimal PriceMin { get; set; }
        public decimal PriceMax { get; set; }
        public string PriceUnit { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập diện tích tối thiểu.")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Diện tích phải là số dương.")]
        public decimal? AreaMin { get; set; }
        public decimal? AreaMax { get; set; }
        public string AreaUnit { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống.")]
        public string Description { get; set; }

        public string Phone { get; set; }

        public IEnumerable<HttpPostedFileBase> ImageFiles { get; set; }
    }
}