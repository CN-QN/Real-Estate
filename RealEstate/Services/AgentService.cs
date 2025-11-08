using RealEstate.Models;
using RealEstate.Models.ViewModels;
using RealEstate.Repository;
using System;
using System.Collections.Generic;
using System.IO;
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
        public GetPropertyDetail_Result GetMyPostDetail(int propertyId, int userId)
        {
            return _AgentRepo.GetMyPostDetail(propertyId, userId);
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
        public bool CreatePost(PostViewModels request,List<HttpPostedFileBase> files, int userId)
        {

            List<string> fileNames = new List<string>();
            foreach(var file in files)
            {
                if(file.ContentLength>0 )
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Content/Uploads/"), fileName);
                    file.SaveAs(path);

                    fileNames.Add(fileName);
                }
            }
            
            
            request.ImageUrls = fileNames;
            return _AgentRepo.CreatePost(request, userId);
        }

        public List<GetPropertyByUser_Result> GetMyPosts(int userId, int pageNumber)
        {
            return _AgentRepo.GetMyPosts( userId,  pageNumber);
        }

        public bool DeletePost(int Id)
        {
            return _AgentRepo.DeletePost(Id);
        }
    }
}