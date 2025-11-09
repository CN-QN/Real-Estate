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
        public GetPropertyDetail_Result GetMyPostDetail(int propertyId )
        {
            var post = db.GetPropertyDetail(propertyId).FirstOrDefault();
            return post;
        }

        public List<Ward> Wards(int? district_code)
        {
            List<Ward> wards = db.Wards.Where(i =>i.district_code== district_code).ToList();
            return wards;
        }
        public void EditPost(GetPropertyDetail_Result model, int Id)
        {
            db.Database.ExecuteSqlCommand(@"
        EXEC UpdateProperty 
            @Id = {0}, 
            @Title = {1}, 
            @Description = {2}, 
            @AreaMax = {3}, 
            @AreaMin = {4}, 
            @AreaUnit = {5}, 
            @TypeId = {6}, 
            @PriceMax = {7}, 
            @PriceMin = {8}, 
            @PriceUnit = {9}, 
            @AddressId = {10},
            @AddressDetail = {11}",
                Id,
                model.Title,
                model.Description,
                model.AreaMax,
                model.AreaMin,
                model.AreaUnit,
                model.TypeId,
                model.PriceMax,
                model.PriceMin,
                model.PriceUnit,
                model.Address_id,
                model.Address
            );
        }

        public List<PropertyViewModel> GetMyPosts(int userId, int pageNumber)
        {
            var posts = db.GetPropertyByUser(userId, pageNumber)
    .Select(p => new PropertyViewModel
    {
        Id = p.Id,
        Title = p.Title,
        AreaMin = p.AreaMin.Value,
        AreaMax = p.AreaMax.Value,
        AreaUnit = p.AreaUnit,
        Name = p.Name,
        PriceMin = p.PriceMin.Value,
        PriceMax = p.PriceMax.Value,
        PriceUnit = p.PriceUnit,
        TypeId = p.TypeId,
        Address = p.Address,
        TotalPage =Convert.ToInt32(p.TotalPage),
        ImageUrl = p.ImageUrl,
        Avatar = p.Avatar,
        Status = p.Status,
        CreatedAt = p.CreatedAt.Value
    })
    .ToList();

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
     request.LoaiBDS,
     request.DonViDienTich,
     request.DonViGia
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