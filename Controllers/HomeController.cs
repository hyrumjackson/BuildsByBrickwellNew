using System;
using System.Diagnostics;
using BuildsByBrickwellNew.Models;
using BuildsByBrickwellNew.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildsByBrickwellNew.Controllers
{
    public class HomeController : Controller
    {
        private IntexProjectContext _context;

        public HomeController(IntexProjectContext temp)
        {
            _context = temp;    
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Products(int pageNum, string? productType, string? productColor, int pageSize = 5)
        {
            // Ensure that the page size is within a valid range
            pageSize = Math.Clamp(pageSize, 5, 20);

            // Query for products based on category, color, and page size filters
            var filteredProducts = _context.Products
                .Where(x => (string.IsNullOrEmpty(productType) || x.Category == productType || productType == "Products") &&
                            (string.IsNullOrEmpty(productColor) || x.PrimaryColor == productColor || x.SecondaryColor == productColor))
                .OrderBy(x => x.Name)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize);

            // Get the total count for pagination
            int totalItems = _context.Products.Count();

            // If a specific category is selected, update the total count
            if (!string.IsNullOrEmpty(productType) && productType != "Products")
            {
                totalItems = _context.Products.Where(x => x.Category == productType).Count();
            }

            var viewModel = new ProductsListViewModel
            {
                Products = filteredProducts,
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = pageNum,
                    ItemsPerPage = pageSize,
                    TotalItems = totalItems
                },
                CurrentProductType = productType,
                CurrentProductColor = productColor
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult HyrumCart()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult OrderStatus()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var productDetails = _context.Products
                .Single(x => x.ProductId == id);

            return View(productDetails);
        }

        public IActionResult Testing()
        {
            var customers = _context.Customers.ToList();

            return View(customers);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult AdminUsers ()
        {
            return View();
        }

        //public IActionResult AdminProducts()
        //{
        //    var products = _context.Products
        //        .ToList();

        //    return View(products);
        //}

        //[HttpGet]
        //public IActionResult ProductForm()
        //{
        //    return View("ProductForm", new Product());
        //}

        //[HttpPost]
        //public IActionResult ProductForm(Product response)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        response.Year = 0;
        //        response.NumParts = 0;
        //        response.Price = 0;
        //        response.ImgLink = "";
        //        _context.Products.Add(response); // add a record to the database
        //        _context.SaveChanges();
        //        return View("Confirmation", response);
        //    }
        //    else
        //    {
        //        return View(response);
        //    }
        //}

        //// HTTP GET action method to display the movie edit form
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    var recordtoEdit = _context.Products
        //        .Single(x => x.ProductId == id);

        //    return View("ProductForm", recordtoEdit);
        //}

        //// HTTP POST action method to handle movie edit submission
        //[HttpPost]
        //public IActionResult Edit(Product updatedInfo)
        //{
        //    _context.Update(updatedInfo);
        //    _context.SaveChanges();
        //    return RedirectToAction("AdminProducts");
        //}

        //// HTTP GET action method to display the movie delete confirmation page
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    var recordtoDelete = _context.Products
        //        .Single(x => x.ProductId == id);

        //    return View(recordtoDelete);
        //}

        //// HTTP POST action method to handle movie deletion
        //[HttpPost]
        //public IActionResult Delete(Product updatedInfo)
        //{
        //    _context.Products.Remove(updatedInfo);
        //    _context.SaveChanges();
        //    return RedirectToAction("AdminProducts");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
