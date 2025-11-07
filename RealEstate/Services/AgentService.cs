using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Services
{
    public class AgentService
    {
        private AgentRepo _AgentRepo = new AgentRepo();
        public List<Province> Provinces()
        {
            return _AgentRepo.Provinces();
        }
        public List<District> Districts(int? province_code)
        {
            if (!province_code.HasValue)
            {
                return null;
            }
            return _AgentRepo.Districts(province_code);
        }
        public List<Ward> Wards(int? district_code)
        {
            if (!district_code.HasValue)
            {
                return null;
            }
            return _AgentRepo.Wards(district_code);
        }
        public bool AddPost(PostViewModels request, int userId)
        {
            return _AgentRepo.AddPost(request, userId);
        }
    }
}