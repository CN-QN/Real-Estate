using RealEstate.Models;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Services
{
    public class AuthService
    {
          AuthRepo _AuthRepo = new AuthRepo();
        public void UpdateResetPassword (ResetPassword model)
        {
             _AuthRepo.CreateResetPassword(model);
        }
        public void VerifyTokenPassword(string token, string email)
        {

            _AuthRepo.VerifyTokenPassword(token, email);
        }
    }
}