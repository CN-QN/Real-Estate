using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage ="Email không hợp lệ")]
        public string Email { get;set;  }
    }
}