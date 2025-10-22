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
                Phone = i.Phone,
                Avatar = i.Avatar,
                Role = new Role()
                {
                    Id = Convert.ToInt32(i.RoleId),
                    Name = Convert.ToString(i.RoleName)
                }
            }).FirstOrDefault();
            return user;
        }
        public User FindEmail(string Email)
        {
            return db.Users.FirstOrDefault(i => i.Email.ToLower().Trim() == Email);
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
                ProviderKey = ProviderKey ?? ""
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
    }
}