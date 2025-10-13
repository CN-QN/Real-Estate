using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class LoginViewModels
    {
        [Required(ErrorMessage = "Vui lòng nhập Email")]

        public string Email { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
    }
}