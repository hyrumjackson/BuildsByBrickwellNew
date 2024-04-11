using System.ComponentModel.DataAnnotations;
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
            [Required]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = Input.Username, // Use the provided username
                    Email = Input.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Errors.Any(e => e.Code == "DuplicateUserName"))
                {
                    ModelState.AddModelError("Input.Username", "Username is already taken.");
                    return Page();
                }

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
                    return RedirectToPage("Login"); // Redirect to home page or login page
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Code == "DuplicateUserName" || error.Code == "DuplicateEmail")
                        {
                            // You might want to differentiate between duplicate username and duplicate email messages
                            ModelState.AddModelError(string.Empty, "User with this email/username already exists.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            // Something failed, redisplay form
            return Page();
        }

    }

}
