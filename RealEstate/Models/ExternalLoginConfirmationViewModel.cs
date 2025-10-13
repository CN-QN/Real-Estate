using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models // Đảm bảo đúng namespace của project bạn
{
    // ViewModel này dùng để hiển thị trang xác nhận/đăng ký sau khi đăng nhập Google thành công
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // Bạn có thể thêm các trường khác như Tên, Mật khẩu (nếu muốn)
        // nhưng thông thường chỉ cần Email để tạo tài khoản mới.
    }
}