using System.Linq;
using System.Web.Mvc;
using RealEstate.Models;  
namespace RealEstate.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext(); 

        
        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
              
                var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);

                if (user != null && user.Password == model.Password) 
                {
                    
                    Session["User"] = user;
                    return RedirectToAction("Index", "Home"); 
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
            }
            return View(model);
        }

        
        public ActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username already exists.");
                    return View(model);
                }

                
                var newUser = new User
                {
                    Username = model.Username,
                    Password = model.Password 
                };

              
                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Login"); 
            }

            return View(model);
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session.Clear(); 
            return RedirectToAction("Index", "Home"); 
        }
    }
}
