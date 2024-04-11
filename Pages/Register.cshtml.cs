using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BuildsByBrickwellNew.Models; // Ensure this using directive is correct for your Customer model

namespace BuildsByBrickwellNew.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IntexProjectContext _context; // Assuming this is your DbContext class name

        // Inject ApplicationDbContext in the constructor
        public RegisterModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IntexProjectContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
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

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
            public string CountryOfResidence { get; set; }
            public string Gender { get; set; }
            public int Age { get; set; }
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

                    // Create the Customer record
                    var customer = new Customer
                    {
                        AspNetUserId = user.Id, // Ensure this property exists in your Customer model
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        BirthDate = Input.BirthDate,
                        CountryOfResidence = Input.CountryOfResidence,
                        Gender = Input.Gender,
                        Age = Input.Age
                    };
                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();

                    // Redirect or sign in the user here
                    return RedirectToPage("Login");
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
