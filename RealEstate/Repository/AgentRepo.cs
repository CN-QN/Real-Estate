using RealEstate.Models;
using System;
using System.Collections.Generic;
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
        public List<Ward> Wards(int? district_code)
        {
            List<Ward> wards = db.Wards.Where(i =>i.district_code== district_code).ToList();
            return wards;
        }
    }
}