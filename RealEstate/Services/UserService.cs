using RealEstate.Models;
using RealEstate.Repository;
using RealEstate.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using BCrypt.Net;

namespace RealEstate.Services
{
    public class UserService
    {
        private UsersRepo _repo = new UsersRepo();
        public int FindEmail(string Email)
        {
           
            return _repo.FindEmail(Email);
        }

        public User VerifyLogin(string Email, string Password)
    {
        var user = _repo.VerifyLogin(Email);
        if (user == null) return null;
        if(BCrypt.Net.BCrypt.Verify(Password, user.Password))
        {
            return user;
            }
        return null;
    }

    public User CreateUser(string Email, string Name, string Password)
    {
         var PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password);
        return _repo.CreateUser(Email, Name, PasswordHash);
    }

        public void UpdatePassword(string Email, string Password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(Password);
            _repo.UpdatePassword(Email, Password);
        }
    }
}