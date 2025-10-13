using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models
{
    public class ResetPassword
    {

        public int UserId { get;set; }
        public string  Token { get; set; }
        public DateTime? TokenExpires { get; set; }
    }
}