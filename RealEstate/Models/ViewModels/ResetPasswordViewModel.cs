using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage ="Mật khẩu phải có ít nhất 6 ký tự")]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage ="Mật khẩu không khớp")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}