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

        public void CreateResetPassword(ResetPassword model)
        {
            db.ResetPasswords.Add(model);
            db.SaveChanges();

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