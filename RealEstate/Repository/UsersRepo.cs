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
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace RealEstate.Repository
{

   
    public class UsersRepo
    {
        RealEstateEntities db = new RealEstateEntities();

        public User VerifyLogin (string Email)
        {
            User user = db.VerifyLogin(Email).Select(i => new User
            {
                Id = i.Id,
                Name = i.Name,
                Email = i.Email,
                Password = i.Password,
                Avatar = i.Avatar,
                Role = new Role()
                {
                    Id = Convert.ToInt32(i.RoleId),
                    Name = Convert.ToString(i.RoleName)
                }
            }).FirstOrDefault();
            return user;
        }
        public UserProfile FindEmail(string Email)
        {
           var user =  db.Users.FirstOrDefault(i => i.Email.ToLower().Trim() == Email);
            var profile = db.UserProfiles.FirstOrDefault(i => i.UserId == user.Id);
            UserProfile userProfile = new UserProfile()
            {
                UserId = user.Id,
                Gender = profile.Gender,
                Address = profile.Address,
                Instagram = profile.Instagram,
                Facebook = profile.Facebook,
                CoverPhoto = profile.CoverPhoto,
                Website = profile.Website,
                Bio = profile.Bio,
                DateOfBirth = profile.DateOfBirth,
                User = user,
                UpdatedAt = profile.UpdatedAt,
                
            };
            return userProfile;
        }

        public User CreateUser(string Email ,string Name,  string Password,string ProviderName , string ProviderKey)
        {

            var newUser = new User()
            {
                Email = Email,
                Name = Name,
                Password = Password,
                RoleId = 1,
                ProviderName = ProviderName ?? "",
                ProviderKey = ProviderKey ?? "",
                CreatedAt = DateTime.Now,
            };

            // Thêm vào DB
            db.Users.Add(newUser);
            db.SaveChanges();

            if (newUser == null) 
                {
                throw new Exception("Tạo tài khoản thất bại !!");
                }
            return newUser;
        }

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

        public void UpdateProvider (string Email , string ProviderName, string ProviderKey)
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
        public User GetUserById(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id == id);
        }

        public void UpdateUserProfile(int id,UserProfile updatedProfile)
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
                    Facebook = updatedProfile.Facebook,
                    UpdatedAt = DateTime.Now,
                    Instagram = updatedProfile.Instagram,
                    Website = updatedProfile.Website,
                    CoverPhoto = updatedProfile.CoverPhoto
                };
                db.UserProfiles.Add(userProfile);
            }

           else
            {
                profile.Gender = updatedProfile.Gender;
                profile.DateOfBirth = updatedProfile.DateOfBirth;
                profile.Bio = updatedProfile.Bio;
                profile.Address = updatedProfile.Address;
                profile.Facebook = updatedProfile.Facebook;
                profile.Instagram = updatedProfile.Instagram;
                profile.Website = updatedProfile.Website;
                profile.CoverPhoto = updatedProfile.CoverPhoto;
                profile.UpdatedAt = DateTime.Now;
            }

                db.SaveChanges();
        }


        public void UpdateName(string Email , string Name)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == Email);
            if (user == null)
            {
                throw new Exception("Không tìm thấy người dùng.");

            }

            user.Name = Name;
            db.SaveChanges();
        }

        
    }
}