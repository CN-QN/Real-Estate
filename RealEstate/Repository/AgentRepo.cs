using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealEstate.Models.ViewModels;
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
        public List<Ward> Wards(int? district_code)
        {
            List<Ward> wards = db.Wards.Where(i =>i.district_code== district_code).ToList();
            return wards;
        }

        public bool AddPost(PostViewModels request, int userId)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // 1. LƯU THÔNG TIN ĐỊA CHỈ (Bảng Address)
                    var newAddress = new Address
                    {
                        Province_Id = request.ProvinceCode,
                        District_Id= request.DistrictCode,
                        Ward_Id = request.WardCode.ToString(),
                        Address_detail = request.AddressDetail, // Địa chỉ chi tiết (số nhà)
                        lat = request.Latitude,
                        lon = request.Longitude,
                        StreetName = request.StreetName
                    };
                    db.Addresses.Add(newAddress);
                    db.SaveChanges(); // Lấy AddressId (Id) cho Properties

                    // 2. LƯU THÔNG TIN BÀI ĐĂNG (Bảng Properties)
                    var newProperty = new Property
                    {
                        UserId = userId,
                        Address_id = newAddress.Id, // Khóa ngoại từ Address
                        Title = request.TenDuAn, // Dùng tên dự án làm tiêu đề tạm thời
                        Description= request.MoTa,
                        PriceMin = request.GiaMin,
                        PriceMax = request.GiaMax,
                        AreaMin = request.DienTichMin,
                        AreaMax = request.DienTichMax,
                        PropertyType = new PropertyType ()
                        { 
                            Id = request.LoaiBDS
                        },

                        Status = "Pending", // Đặt trạng thái chờ duyệt
                        StartDate = request.NgayBatDau,
                        EndDate = request.NgayBatDau.AddDays(request.ThoiGianTuan * 7), // Tính ngày kết thúc

                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };
                    db.Properties.Add(newProperty);
                    db.SaveChanges(); // Lấy PropertyId (Id) cho PropertyImages

                    // 3. LƯU DANH SÁCH ẢNH (Bảng PropertyImages)
                    if (request.ImageUrls != null && request.ImageUrls.Any())
                    {
                        int order = 1;
                        foreach (var url in request.ImageUrls)
                        {
                            var newImage = new PropertyImage
                            {
                                PropertyId = newProperty.Id, // Khóa ngoại từ Properties
                                ImageUrl= url,
                                CreatedAt = DateTime.Now,
                            };
                            db.PropertyImages.Add(newImage);
                        }
                        db.SaveChanges();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log lỗi (rất quan trọng)
                    System.Diagnostics.Debug.WriteLine($"Lỗi khi thêm bài đăng: {ex.Message}");
                    return false;
                }
            }
        }
    }
}