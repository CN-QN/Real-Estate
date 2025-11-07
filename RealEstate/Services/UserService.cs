using BCrypt.Net;
using Microsoft.AspNet.Identity;
using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Repository;
using RealEstate.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace RealEstate.Services
{
    public class UserService
    {
        private UsersRepo _repo = new UsersRepo();
        public UserProfile FindEmail(string Email)
        {

            return _repo.FindEmail(Email.ToLower().Trim());
        }

        public User VerifyLogin(string Email, string Password)
        {
            var user = _repo.VerifyLogin(Email);
            if (user == null) return null;
            if (BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {
                return user;
            }
            return null;
        }

        public User CreateUser(string Email, string Name, string Password, string ProviderName, string Providerkey)
        {
            if(Password !=null && ProviderName == null && Providerkey ==null)
            {
                var PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password);
                return _repo.CreateUser(Email, Name, PasswordHash, null, null);


            }
            return _repo.CreateUser(Email, Name, null, ProviderName, Providerkey);

        }

     


        public void UpdateProvider(string Email, string Provider, string ProviderKey)
        {
            _repo.UpdateProvider(Email, Provider, ProviderKey);
        }
        public User GetProfile(int userId)
        {
            return _repo.GetUserById(userId);
        }

         public void UpdateFullProfile(int id , ProfileViewModel model)
        {
            _repo.UpdateUserProfile(id,new UserProfile
            {
                UserId = model.Id,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Bio = model.Bio,
                Address = model.Address,
                Facebook = model.Facebook,
                Instagram = model.Instagram,
                Website = model.Website,
                CoverPhoto = model.CoverPhoto,
                UpdatedAt = DateTime.Now
            });
        }
 
        public void UpdatePassword(string Email, string Password)
        {
            
            Password = BCrypt.Net.BCrypt.HashPassword(Password);
            _repo.UpdatePassword(Email, Password);


          
        }
        public void UpdateName(string Email, string Name)
        {
            _repo.UpdateName(Email, Name);
        }
        
    }
}