using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using RealEstate.Models.ViewModels;

namespace RealEstate.Models
{
    public class PostService : IDisposable
    {
        private readonly RealEstateEntities db = new RealEstateEntities();

        private static int? ToIntOrNull(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            int v; return int.TryParse(s, out v) ? (int?)v : null;
        }

        public bool CreatePost(PostFormVM model)
        {
            if (model == null) return false;

            var newPost = new Post
            {
                ProjectName = model.ProjectName?.Trim(),
                TransactionType = model.TransactionType?.Trim(),
                PropertyType = model.PropertyType?.Trim(),
                Title = model.Title?.Trim(),
                Description = model.Description,
                ProvinceCode = ToIntOrNull(model.ProvinceCode),
                DistrictCode = ToIntOrNull(model.DistrictCode),
                WardCode = ToIntOrNull(model.WardCode),
                StreetName = model.StreetName?.Trim(),
                AddressText = model.AddressText?.Trim(),
                Direction = model.Direction?.Trim(),
                PriceMin = model.PriceMin,
                PriceMax = model.PriceMax,
                AreaMin = model.AreaMin,
                AreaMax = model.AreaMax,
                Status = "Pending",
                CreatedAt = DateTime.Now,
                Images = new List<PostImage>()
            };

            var http = HttpContext.Current;
            if (http == null) return false;

            var folder = http.Server.MapPath("~/Uploads/PostImages/");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var allowedExt = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            const int maxBytes = 10 * 1024 * 1024;

            using (var tx = db.Database.BeginTransaction())
            {
                try
                {
                    db.Set<Post>().Add(newPost);
                    db.SaveChanges();

                    if (model.ImageFiles != null)
                    {
                        foreach (var f in model.ImageFiles.Where(x => x != null && x.ContentLength > 0))
                        {
                            var ext = Path.GetExtension(f.FileName);
                            if (string.IsNullOrEmpty(ext) || !allowedExt.Contains(ext)) continue;
                            if (f.ContentLength > maxBytes) continue;

                            var fname = $"{Guid.NewGuid():N}{ext}";
                            var path = Path.Combine(folder, fname);
                            f.SaveAs(path);

                            newPost.Images.Add(new PostImage { FilePath = "/Uploads/PostImages/" + fname });
                        }
                    }

                    db.SaveChanges();
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    return false;
                }
            }
        }

        public void Dispose()
        {
            db?.Dispose();
        }
    }
}
