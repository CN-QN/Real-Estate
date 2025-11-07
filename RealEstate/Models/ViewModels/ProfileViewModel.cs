using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Models.ViewModels
{

    public class ProfileViewModel
    {
        // --- User table ---
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGoogleAccount => ProviderName == "Google";
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        // --- UserProfile table ---
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Website { get; set; }
        public string CoverPhoto { get; set; } // URL ảnh bìa
    }


}