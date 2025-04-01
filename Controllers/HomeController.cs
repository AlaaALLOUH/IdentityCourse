using System.Diagnostics;
using IdentityCourse.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IdentityCourse.Data;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.ValueGeneration.Internal;

namespace IdentityCourse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public UserManager<IdentityUser> _userManager;
       public RoleManager<IdentityRole>_roleManager;
        public ApplicationDbContext db;
        public HomeController(ILogger<HomeController> logger ,UserManager<IdentityUser> user , RoleManager<IdentityRole> role , ApplicationDbContext identity)
        {
            _logger = logger;
            _userManager= user;
           _roleManager = role;
            db = identity;
        }

        public async Task<IActionResult> Index()
        {
        
            //await rolemanamger.CreateAsync(new Identit"yRole { Name = "Admin" });
            //await rolemanamger.CreateAsync(new IdentityRole { Name = "superAdmin" });
            //await rolemanamger.CreateAsync(new IdentityRole { Name = "salesManager" });
            //Another way by dependency injection : 
            if (db.Roles.FirstOrDefault(x => x.Name == "Alaa")==null)
            {
                db.Roles.Add(new IdentityRole { Name = "Alaa" });
                db.SaveChanges();
            }
            if (User.Identity.IsAuthenticated)
            {
                HttpContext.Session.SetString("userEmail",User .Identity.Name); 
            }
            else
            {
                HttpContext.Session.SetString("userEmail","");
            }
            return View();
        }
        public async Task<IActionResult> UserRole()
        {
            
            var users = await _userManager.Users.ToListAsync();
            
                List<UserRoleVm> result = new List<UserRoleVm>();

                foreach (var item in users)
                {
                if (User != null)
                {
                    var roles = await _userManager.GetRolesAsync(item);
                    result.Add(new UserRoleVm { User = item, userRoles = roles.ToList() });
                }
                }

                ViewBag.AllRoles = _roleManager.Roles.ToList();

                return View(result);

            
        }
        
        public async Task<IActionResult> addroletouser(string userid , string rolename)
        {
           

            var user = await _userManager.FindByIdAsync(userid); //I have the current user that i want to work on it 
            //want to add role to user 
            var result = await _userManager.AddToRoleAsync(user, rolename);
           
                if (!result.Succeeded)
                {
               
                    await _userManager.RemoveFromRoleAsync(user, rolename);
                }
            
          return RedirectToAction("UserRole");
        }
        //add role to db by user
        public async Task<IActionResult> addRole(roleVM model)
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = model.roleName });
            return RedirectToAction("Index"); 
        }
        public IActionResult users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        public IActionResult roles()
        {
           var role=_roleManager.Roles.ToList();
            return View(role);
        }
        [Authorize (Roles = "Admin")]
        public IActionResult sales()
        {

            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
