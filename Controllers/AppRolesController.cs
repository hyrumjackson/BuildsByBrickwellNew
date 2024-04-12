/*using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BuildsByBrickwellNew.Controllers
{
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //List all the roles created by users 
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Create (IdentityRole model)
        {
            //avoid duplicate roles
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter)
        }
    }
}*/
