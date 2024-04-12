using System.Diagnostics;
using System.Threading.Tasks;
using BuildsByBrickwellNew.Models;
using BuildsByBrickwellNew.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuildsByBrickwellNew.Controllers
{
    public class AdminController : Controller
    {
        private readonly IntexProjectContext _context;

        // A variable defined that defines the _userManager
        private readonly UserManager<IdentityUser> _userManager; // Add this line

        // Update your constructor to include UserManager
        public AdminController(IntexProjectContext temp, UserManager<IdentityUser> userManager)
        {
            _context = temp;
            _userManager = userManager; // Initialize _userManager
        }

        public IActionResult AdminProducts()
        {
            var products = _context.Products
                .ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult ProductForm()
        {
            return View("ProductForm", new Product());
        }

        [HttpPost]
        public IActionResult ProductForm(Product response)
        {
            if (ModelState.IsValid)
            {
                response.Year = 0;
                response.NumParts = 0;
                response.Price = 0;
                response.ImgLink = "";
                _context.Products.Add(response); // add a record to the database
                _context.SaveChanges();
                return View("Confirmation", response);
            }
            else
            {
                return View(response);
            }
        }

        // HTTP GET action method to display the movie edit form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordtoEdit = _context.Products
                .Single(x => x.ProductId == id);

            return View("ProductForm", recordtoEdit);
        }

        // HTTP POST action method to handle movie edit submission
        [HttpPost]
        public IActionResult Edit(Product updatedInfo)
        {
            _context.Update(updatedInfo);
            _context.SaveChanges();
            return RedirectToAction("AdminProducts");
        }

        // HTTP GET action method to display the movie delete confirmation page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordtoDelete = _context.Products
                .Single(x => x.ProductId == id);

            return View(recordtoDelete);
        }

        // HTTP POST action method to handle movie deletion
        [HttpPost]
        public IActionResult Delete(Product updatedInfo)
        {
            _context.Products.Remove(updatedInfo);
            _context.SaveChanges();
            return RedirectToAction("AdminProducts");
        }

        public async Task<IActionResult> AdminUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }

        /*        [HttpGet]*/
        /*public IActionResult Edit(int id)
        {
            var recordtoEdit = _context.Products
                .Single(x => x.ProductId == id);

            return View("ProductForm", recordtoEdit);
        }

        // HTTP POST action method to handle movie edit submission
        [HttpPost]
        public IActionResult Edit(Product updatedInfo)
        {
            _context.Update(updatedInfo);
            _context.SaveChanges();
            return RedirectToAction("AdminProducts");
        }*/

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Create and populate a view model if needed
            // Return the view for editing the user

            return View("UserForm", user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(IdentityUser updatedInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Update(updatedInfo);
                _context.SaveChanges();
                return RedirectToAction("AdminUsers");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Create and populate a view model if needed
            // Return the view for editing the user

            return View( user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(IdentityUser updatedInfo)
        {
            _context.Remove(updatedInfo);
            _context.SaveChanges();
            return RedirectToAction("AdminUsers");
        }



    }
}
