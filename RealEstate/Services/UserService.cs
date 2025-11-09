using BCrypt.Net;
using Microsoft.AspNet.Identity;
using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Repository;
using RealEstate.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;

namespace RealEstate.Services
{
    public class UserService
    {
        private UsersRepo _UserRepo= new UsersRepo();






        public UserProfileVIewModel GetProfile(string Email)
        {
            return _UserRepo.GetProfile(Email);
        }

        public void UpdateProvider(string Email, string Provider, string ProviderKey)
        {
            _UserRepo.UpdateProvider(Email, Provider, ProviderKey);
        }
      

         public void UpdateFullProfile(int id , UserProfileVIewModel model)
        {
            _UserRepo.UpdateUserProfile(id, new UserProfileVIewModel
            {
                Id = model.Id,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                Bio = model.Bio,
                Address = model.Address,
                UpdatedAt = DateTime.Now
            });
        }
 
        public void UpdatePassword(string Email, string Password)
        {
            
            Password = BCrypt.Net.BCrypt.HashPassword(Password);
            _UserRepo.UpdatePassword(Email, Password);


          
        }
        public void UpdateName(string Email, string Name)
        {
            _UserRepo.UpdateName(Email, Name);
        }
        
    }
}