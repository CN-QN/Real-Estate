using RealEstate.Models;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace RealEstate.Services
{
    public class AuthService
    {
        private AuthRepo _AuthRepo = new AuthRepo();

        public void UpdateResetPassword (ResetPassword model)
        {
             _AuthRepo.CreateResetPassword(model);
        }
    
        public void VerifyTokenPassword(string token, string email)
        {

            _AuthRepo.VerifyTokenPassword(token, email);
        }
        public User FindEmail(string Email)
        {


            return _AuthRepo.FindEmail(Email.ToLower().Trim());
        }
        public User VerifyLogin(string Email, string Password)
        {
            var user = _AuthRepo.VerifyLogin(Email);
            if(user == null)
            {
                throw  new Exception ("User chưa tồn tại");
            }
            if (BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {
                return user;
            }
            return null;
        }

        public User CreateUser(string Email, string Name, string Password, string ProviderName, string Providerkey)
        {
            if (Password != null && ProviderName == null && Providerkey == null)
            {
                var PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password);
                return _AuthRepo.CreateUser(Email, Name, PasswordHash, null, null);


            }
            return _AuthRepo.CreateUser(Email, Name, null, ProviderName, Providerkey);

        }
    }
}