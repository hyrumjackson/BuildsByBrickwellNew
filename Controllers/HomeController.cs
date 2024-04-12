using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using BuildsByBrickwellNew.Models;
using BuildsByBrickwellNew.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;


namespace BuildsByBrickwellNew.Controllers
{
    public class HomeController : Controller
    {
        private IntexProjectContext _context;

        public HomeController(IntexProjectContext temp)
        {
            _context = temp;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> productsToDisplay = new List<Product>();

            if (User.Identity.IsAuthenticated)
            {
                var userEmail = User.Identity.Name;

                if (userEmail == "krysten.froehlich@gmail.com")
                {
                    var customerRec = await _context.Customer2_Recs.FirstOrDefaultAsync(); // Fetch the single row from the table

                    if (customerRec != null)
                    {
                        var recommendedProductIds = new List<int?> { customerRec.Rec1, customerRec.Rec2, customerRec.Rec3 }
                            .Where(id => id.HasValue)
                            .Select(id => id.Value)
                            .ToList();

                        productsToDisplay = await _context.Products
                            .Where(p => recommendedProductIds.Contains(p.ProductId))
                            .ToListAsync();
                    }
                    else
                    {
                        // Fallback to top rated products if no customer recommendation data is found
                        productsToDisplay = await GetTopRatedProducts();
                    }
                }
                else
                {
                    // For other users, fetch the top three entries from Auth_new_user_rec
                    var newUserRecs = await _context.Auth_New_User_Recs
                        .Select(x => (int)x.ProductId)
                        .Take(3) // Assuming we take top three
                        .ToListAsync();

                    productsToDisplay = await _context.Products
                        .Where(p => newUserRecs.Contains(p.ProductId))
                        .ToListAsync();
                }
            }
            else
            {
                // For non-logged-in users
                productsToDisplay = await GetTopRatedProducts();
            }

            return View(productsToDisplay);
        }

        private async Task<List<Product>> GetTopRatedProducts()
        {
            var highRatedProductIds = await _context.High_Rated_Recs
                .OrderByDescending(p => p.Rating)
                .Take(3)
                .Select(p => p.ProductId)
                .ToListAsync();

            return await _context.Products
                .Where(p => highRatedProductIds.Contains((byte)p.ProductId))  // Cast int to byte here
                .ToListAsync();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Products(int pageNum, string? productType, string? productColor, int pageSize)
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

        public IActionResult Orders()
        {

            var records = _context.Orders
                .OrderByDescending(o => o.Date)
                .Take(20)
                .ToList();
            return View(records);
        }

        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Checkout(string total)
        {
            // Parse the currency value
            if (decimal.TryParse(total, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal parsedTotal))
            {
                var order = new Order
                {
                    Amount = (double?)parsedTotal
                };
                return View(order);
            }

            return View(new Order()); // fallback if parsing fails
        }
        [HttpPost]
        public IActionResult Checkout(Order response)
        {
            if (ModelState.IsValid)
            {
                _context.Orders.Add(response);
                _context.SaveChanges();
                return RedirectToAction("ReviewOrders", "ONNX");
            }
            return View(response);
        }

        public IActionResult OrderStatus()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            // Fetch the main product details
            var productDetails = await _context.Products
                                               .SingleOrDefaultAsync(x => x.ProductId == id);

            if (productDetails == null)
            {
                return NotFound(); // If no product is found, return a NotFound result.
            }

            // Fetch recommended product IDs from the item-based recommendations table
            var itemRecs = await _context.Item_Based_Recs
                                         .FirstOrDefaultAsync(x => x.ProductId == id);

            List<Product> recommendedProducts = new List<Product>();
            if (itemRecs != null)
            {
                // List to hold potential recommended product IDs
                List<int> recProductIds = new List<int>();

                // Adding recommendations to the list if they exist
                if (itemRecs.RecommendedProductId1.HasValue)
                    recProductIds.Add(itemRecs.RecommendedProductId1.Value);
                if (itemRecs.RecommendedProductId2.HasValue)
                    recProductIds.Add(itemRecs.RecommendedProductId2.Value);
                if (itemRecs.RecommendedProductId3.HasValue)
                    recProductIds.Add(itemRecs.RecommendedProductId3.Value);

                // Fetch the recommended products from the database
                recommendedProducts = await _context.Products
                                                    .Where(p => recProductIds.Contains(p.ProductId))
                                                    .ToListAsync();
            }

            // Use a ViewModel or ViewBag/ViewData to pass both the product details and the recommended products to the view
            ViewBag.RecommendedProducts = recommendedProducts;

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
