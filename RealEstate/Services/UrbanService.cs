using RealEstate.Models.ViewModels;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Services
{
    public class UrbanService
    {
        private UrbanRepo _UrbanRepo = new UrbanRepo();
        public List<UrbanViewModel> GetUrbanAll()
        {
            return _UrbanRepo.GetUrbanAll();
        }
        public UrbanDetailViewModel GetUrbanDetails(int id)
        {
            return _UrbanRepo.GetUrbanDetails(id);
        }
    }
}