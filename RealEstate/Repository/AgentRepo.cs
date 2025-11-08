using RealEstate.Models;
using RealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace RealEstate.Repository
{
    public class AgentRepo
    {
        RealEstateEntities db = new RealEstateEntities();
        public List<Province> Provinces ()
        {
            List<Province> provinces = db.Provinces.ToList();
            return provinces;
        }
        public List<District> Districts(int? province_code)
        {
            List<District> districts = db.Districts.Where(i =>i.province_code == province_code).ToList();
            return districts;
        }
        public GetPropertyDetail_Result GetMyPostDetail(int propertyId, int userId)
        {
            var post = db.GetPropertyDetail(propertyId, userId).FirstOrDefault();
            return post;
        }

        public List<Ward> Wards(int? district_code)
        {
            List<Ward> wards = db.Wards.Where(i =>i.district_code== district_code).ToList();
            return wards;
        }
        public List<GetPropertyByUser_Result> GetMyPosts(int userId, int pageNumber)
        {
            var posts =  db.GetPropertyByUser(userId, pageNumber).ToList();
            return posts;
        }
        public bool CreatePost(PostViewModels request, int userId)
        {
            var propertyId = db.CreatePost(
     userId,
     request.ProvinceCode,
     request.DistrictCode,
     request.WardCode,
     request.AddressDetail,
     request.Latitude,
     request.Longitude,
     request.StreetName,
     request.TenDuAn,
     request.MoTa,
     request.GiaMin,
     request.GiaMax,
     request.DienTichMin,
     request.DienTichMax,
     request.LoaiBDS
 ).FirstOrDefault();


            if (propertyId.HasValue)
            {
                foreach (var url in request.ImageUrls)
                {
                    if (!string.IsNullOrEmpty(url))
                    {
                        db.PropertyImages.Add(new PropertyImage
                        {
                            PropertyId = propertyId.Value,
                            ImageUrl = url,
                            CreatedAt = DateTime.Now
                        });
                    }
                }
                db.SaveChanges();
            }
            else
            {
                throw new Exception("Không tạo được property, không thể thêm ảnh.");
            }
            return true;
        }

        public bool DeletePost(int id)
        {
            var entity = db.Properties.Find(id);
            if (entity == null)
                return false;
            db.Properties.Remove(entity);
            db.SaveChanges();
            return true;
        }
    }
}