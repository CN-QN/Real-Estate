using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class RegisterViewModels
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [MinLength(2,ErrorMessage ="Tên đăng nhập có ít nhất 2 ký tự")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage="Vui lòng nhập Email hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        [MinLength(6,ErrorMessage ="Mật khẩu có ít nhất 6 ký tự")]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}