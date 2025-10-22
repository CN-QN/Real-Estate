using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    public class MapController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        // GET: /Map
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SearchAddress(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return Json(new { error = "Missing query" }, JsonRequestBehavior.AllowGet);

            string url = $"https://nominatim.openstreetmap.org/search?format=json&q={q}&addressdetails=1&limit=5&countrycodes=vn";
            client.DefaultRequestHeaders.UserAgent.ParseAdd("YourAppName/1.0"); // Bắt buộc với OSM

            HttpResponseMessage response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Lỗi: {response.StatusCode}");
                return new EmptyResult();
     
             }
            string body = await response.Content.ReadAsStringAsync();
            return Content(body);

        }
    }
}