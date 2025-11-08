using Microsoft.AspNet.Identity;
using RealEstate.Models;
using RealEstate.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using RealEstate.Models.ViewModels;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace RealEstate.Repository
{


    public class UsersRepo
    {
        RealEstateEntities db = new RealEstateEntities();






        public void UpdatePassword(string Email, string Password)
        {
            var user = db.Users.FirstOrDefault(i => i.Email == Email);
            if (user == null)
            {
                throw new Exception("Cập nhật mật khẩu thất bại");

            }
            user.Password = Password;
            db.SaveChanges();



        }

        public void UpdateProvider(string Email, string ProviderName, string ProviderKey)
        {
            var user = db.Users.FirstOrDefault(i => i.Email == Email);
            if (user == null)
            {
                throw new Exception(Email + " Cập nhật Provider thất bại");
            }
            user.ProviderName = ProviderName;
            user.ProviderKey = ProviderKey;
            db.SaveChanges();


        }
      

        public void UpdateUserProfile(int id, UserProfileVIewModel updatedProfile)
        {
            var profile = db.UserProfiles.FirstOrDefault(p => p.UserId == id);

            if (profile == null)
            {
                // Nếu user chưa có profile, tạo mới
                UserProfile userProfile = new UserProfile()
                {
                    UserId = id,
                    Gender = updatedProfile.Gender,
                    DateOfBirth = updatedProfile.DateOfBirth,
                    Bio = updatedProfile.Bio,
                    Address = updatedProfile.Address,
                    Phone = updatedProfile.Phone,
                };
                db.UserProfiles.Add(userProfile);
            }

            else
            {
                profile.Gender = updatedProfile.Gender;
                profile.DateOfBirth = updatedProfile.DateOfBirth;
                profile.Bio = updatedProfile.Bio;
                profile.Address = updatedProfile.Address;
                profile.UpdatedAt = DateTime.Now;
            }

            db.SaveChanges();
        }


        public void UpdateName(string Email, string Name)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == Email);
            if (user == null)
            {
                throw new Exception("Không tìm thấy người dùng.");

            }

            user.Name = Name;
            db.SaveChanges();
        }

        public UserProfileVIewModel GetProfile(string Email)
        {
 
            var profile = db.GetProfile(Email).FirstOrDefault();
            UserProfileVIewModel userProfile = new UserProfileVIewModel()
            {
                Id = profile.Id,
                Bio = profile.Bio,
                Phone = profile.Phone,
                Avatar = profile.Avatar,
                Email = profile.Email,
                Address = profile.Address,
                Name = profile.Name,
                Gender=profile.Gender,
                DateOfBirth = profile.DateOfBirth,
                ProviderName = profile.ProviderName,
            };
            return userProfile;

        }
    }
}