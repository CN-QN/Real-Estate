using RealEstate.Models;
using RealEstate.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RealEstate.Repository
{
    public class AuthRepo
    {
        RealEstateEntities db = new RealEstateEntities();


        public User VerifyLogin(string Email)
        {
            User user = db.VerifyLogin(Email).Select(i => new User
            {
                Id = i.Id,
                Name = i.Name,
                Email = i.Email,
                Password = i.Password,
                Role = new Role()
                {
                    Id = Convert.ToInt32(i.RoleId),
                    Name = Convert.ToString(i.RoleName)
                }
            }).FirstOrDefault();
            return user;
        }

        public User CreateUser(string Email, string Name, string Password, string ProviderName, string ProviderKey)
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
        public void CreateResetPassword(ResetPassword model)
        {
            db.ResetPasswords.Add(model);
            db.SaveChanges();

        }

        public User FindEmail(string Email)
        {
            var user = db.Users.FirstOrDefault(i => i.Email == Email);
            //var profile = db.UserProfiles.FirstOrDefault(i => i.UserId == user.Id);
            //UserProfile userProfile = new UserProfile()
            //{
            //    UserId = user.Id,
            //    Gender = profile.Gender,
            //    Address = profile.Address,
            //    Instagram = profile.Instagram,
            //    Facebook = profile.Facebook,
            //    CoverPhoto = profile.CoverPhoto,
            //    Website = profile.Website,
            //    Bio = profile.Bio,
            //    DateOfBirth = profile.DateOfBirth,
            //    User = user,
            //    UpdatedAt = profile.UpdatedAt,

            //};
            //return userProfile;
            return user;

        }
        public void VerifyTokenPassword(string token,string email )
        {
            var Token = db.ResetPasswords.Where(x =>x.Token == token && x.User.Email == email && x.Expires > DateTime.Now ).FirstOrDefault();
         
            if (Token == null)
            {
                 throw new Exception("Token không hợp lệ hoặc đã hết hạn");

            }
        }
    }
}