using RealEstate.Models;
using RealEstate.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealEstate.Repository
{
    public class AdminRepo
    {
        private readonly RealEstateEntities db = new RealEstateEntities();

        public object TinDangMoiThang()
        {
            var data = db.Properties
                .GroupBy(x => new { x.CreatedAt.Value.Year, x.CreatedAt.Value.Month })
                .Select(x => new
                {
                    Thang = x.Key.Month + "/" + x.Key.Year,
                    LuotTin = x.Count()
                })
                .OrderBy(x => x.Thang)
                .ToList();

            return data;
        }
        public List<DanhSachNguoiDung> DanhSachNguoiDung( int? pageNumber)
        {
            IQueryable<DanhSachNguoiDung> query = db.Users
                .Select(x => new DanhSachNguoiDung
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Role = x.Role.Name,
                    Created_at = x.CreatedAt.Value,
                    TongSoNguoiDung = db.Users.Count()
                })
                .OrderByDescending(x => x.Created_at)
                .Skip((pageNumber.Value - 1) * 30)
                .Take(30);

            

            return query.ToList();
        }
        public object TiLeDangTin()
        {
            var data = db.Properties
                .GroupBy(x => x.PropertyType)
                .Select(x => new
                {
                    Loai = x.Key.Name,
                    LuotTin = x.Count()
                })
                .OrderBy(x => x.Loai)
                .ToList();

            return data;
        }

        public TinDangGanDay FindPostById(int id)
        {
            return db.Properties
                .Where(x => x.Id == id)
                .Select(x => new TinDangGanDay
                {
                    Id = x.Id,
                    Title = x.Title,
                    Name = x.User.Name,
                    Date = x.CreatedAt.Value,
                    Status = x.Status
                })
                .FirstOrDefault();
        }

        public bool EditPost(TinDangGanDay model)
        {
            var entity = db.Properties.Find(model.Id);
            if (entity == null)
                return false;

            entity.Title = model.Title;
            entity.Status = model.Status;
            entity.UpdatedAt = DateTime.Now;

            db.SaveChanges();
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

        public TangTruongViewModel TangTruong(int thangNay, int thangTruoc, int namNay, int namTruoc)
        {
            var tongNguoiDung = db.Users.Count();
            var tongTinDang = db.Properties.Count();

            var countNguoiThangNay = db.Users.Count(x =>
                x.CreatedAt.Value.Month == thangNay && x.CreatedAt.Value.Year == namNay);

            var countNguoiThangTruoc = db.Users.Count(x =>
                x.CreatedAt.Value.Month == thangTruoc && x.CreatedAt.Value.Year == namTruoc);

            var countTinThangNay = db.Properties.Count(x =>
                x.CreatedAt.Value.Month == thangNay && x.CreatedAt.Value.Year == namNay);

            var countTinThangTruoc = db.Properties.Count(x =>
                x.CreatedAt.Value.Month == thangTruoc && x.CreatedAt.Value.Year == namTruoc);

            // --- Tính tỉ lệ bài đăng ---
            double tiLeBaiDang;
            if (countTinThangNay > 0 && countTinThangTruoc > 0)
                tiLeBaiDang = ((double)(countTinThangNay - countTinThangTruoc) / countTinThangTruoc) * 100;
            else if (countTinThangNay > 0 && countTinThangTruoc == 0)
                tiLeBaiDang = 100;
            else if (countTinThangNay == 0 && countTinThangTruoc == 0)
                tiLeBaiDang = 0;
            else
                tiLeBaiDang = -100;

            // --- Tính tỉ lệ người dùng ---
            double tiLeNguoiDung;
            if (countNguoiThangNay > 0 && countNguoiThangTruoc > 0)
                tiLeNguoiDung = ((double)(countNguoiThangNay - countNguoiThangTruoc) / countNguoiThangTruoc) * 100;
            else if (countNguoiThangNay > 0 && countNguoiThangTruoc == 0)
                tiLeNguoiDung = 100;
            else if (countNguoiThangNay == 0 && countNguoiThangTruoc == 0)
                tiLeNguoiDung = 0;
            else
                tiLeNguoiDung = -100;

            return new TangTruongViewModel
            {
                TiLeBaiDang = Math.Round(tiLeBaiDang, 2),
                TiLeNguoiDung = Math.Round(tiLeNguoiDung, 2),
                TongBaiDang = tongTinDang,
                TongNguoiDung = tongNguoiDung
            };
        }

        public List<TinDangGanDay> TinDangGanDay(int? take, int? pageNumber)
        {
            IQueryable<TinDangGanDay> query = db.Properties
                .Select(x => new TinDangGanDay
                {
                    Id = x.Id,
                    Title = x.Title,
                    Name = x.User.Name,
                    Date = x.CreatedAt.Value,
                    Status = x.Status,
                    TongSoPost = db.Properties.Count()
                })
                .OrderByDescending(x => x.Date);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            else if (pageNumber.HasValue)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * 30)
                    .Take(30);
            }

            return query.ToList();
        }
        public DanhSachNguoiDung FindUserById(int id)
        {
            return db.Users
                .Where(x => x.Id == id)
                .Select(x => new DanhSachNguoiDung
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Role = x.Role.Name,
                    Created_at = x.CreatedAt.Value
                })
                .FirstOrDefault();
        }

        public bool EditUser(DanhSachNguoiDung model)
        {
            var user = db.Users.Find(model.Id);
            if (user == null)
                return false;

            user.Name = model.Name;
            user.Email = model.Email;

            var role = db.Roles.FirstOrDefault(r => r.Name == model.Role);
            if (role != null)
                user.RoleId = role.Id;

            user.UpdatedAt = DateTime.Now;

            db.SaveChanges();
            return true;
        }

        public bool DeleteUser(int id)
        {
            var user = db.Users.Include(u => u.Properties).FirstOrDefault(u => u.Id == id);
            if (user == null)
                return false;

            // Nếu có bài đăng liên quan thì xóa trước (nếu cần)
            if (user.Properties.Any())
                db.Properties.RemoveRange(user.Properties);

            db.Users.Remove(user);
            db.SaveChanges();
            return true;
        }

        public List<Role> GetAllRoles()
        {
            return db.Roles.ToList();
        }
    }
}
