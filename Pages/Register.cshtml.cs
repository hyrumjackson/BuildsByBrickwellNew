using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildsByBrickwellNew.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Check if the "Customer" role exists
                    if (!await _roleManager.RoleExistsAsync("Customer"))
                    {
                        // Create the "Customer" role if it doesn't exist
                        await _roleManager.CreateAsync(new IdentityRole("Customer"));
                    }

                    // Add the user to the "Customer" role
                    await _userManager.AddToRoleAsync(user, "Customer");

                    // Optionally, sign in the user here
                    return RedirectToPage("Index"); // Redirect to home page or login page
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Something failed, redisplay form
            return Page();
        }

    }

}
