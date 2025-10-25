using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RealEstate.Services
{
    public class AdminService
    {
        private readonly AdminRepo _AdminRepo = new AdminRepo();

        public object TinDangMoiThang()
        {
            return _AdminRepo.TinDangMoiThang();
        }

        public object TiLeDangTin()
        {
            return _AdminRepo.TiLeDangTin();
        }
        public List<DanhSachNguoiDung> DanhSachNguoiDung( int? PageNumber)
        {
            return _AdminRepo.DanhSachNguoiDung( PageNumber);
        }
        public TangTruongViewModel TangTruong()
        {
            var now = DateTime.Now;
            int thangNay = now.Month;
            int namNay = now.Year;
            int thangTruoc = thangNay == 1 ? 12 : thangNay - 1;
            int namTruoc = thangNay == 1 ? namNay - 1 : namNay;

            return _AdminRepo.TangTruong(thangNay, thangTruoc, namNay, namTruoc);
        }

        public TinDangGanDay FindPostById(int id)
        {
            return _AdminRepo.FindPostById(id);
        }

        [HttpGet]
        public bool EditPost(TinDangGanDay model)
        {
            return _AdminRepo.EditPost(model);
        }

        public bool DeletePost(int id)
        {
            return _AdminRepo.DeletePost(id);
        }

        public bool SuaTin(TinDangGanDay model)
        {
            return _AdminRepo.EditPost(model);
        }

        public bool XoaTin(int id)
        {
            return _AdminRepo.DeletePost(id);
        }

        public List<TinDangGanDay> TinDangGanDay(int? Take, int? PageNumber)
        {
            return _AdminRepo.TinDangGanDay(Take, PageNumber);
        }
        public DanhSachNguoiDung FindUserById(int id)
        {
            return _AdminRepo.FindUserById(id);
        }

        public bool EditUser(DanhSachNguoiDung model)
        {
            return _AdminRepo.EditUser(model);
        }

        public bool DeleteUser(int id)
        {
            return _AdminRepo.DeleteUser(id);
        }

        public List<Role> GetAllRoles()
        {
            return _AdminRepo.GetAllRoles();
        }
    }
}
