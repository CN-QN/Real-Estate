using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealEstate.Controllers
{
    [Authorize(Roles ="Admin,Agents")]
    public class DashboardController:Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}