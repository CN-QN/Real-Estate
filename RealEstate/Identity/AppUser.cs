using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Identity
{
    public class AppUser : IdentityUser
    {
        public DateTime? BirthhDay {get;set;}
        public string Address {get;set; }
        public string City {get;set; }

    }
}