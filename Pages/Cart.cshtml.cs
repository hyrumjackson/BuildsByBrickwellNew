using BuildsByBrickwellNew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildsByBrickwellNew.Pages
{
    public class CartModel : PageModel
    {
        private IntexProjectContext _context;
        public Cart Cart { get; set; }

        public CartModel(IntexProjectContext temp, Cart cartService)
        {
            _context = temp;
            Cart = cartService;
        }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
        }

        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product p = _context.Products
                .FirstOrDefault(x => x.ProductId == productId);

            if (p != null)
            {
                Cart.AddItem(p, 1);
            }

            return RedirectToPage(new { returnUrl = returnUrl });
        }

        public IActionResult OnPostRemove(int productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(x => x.Product.ProductId == productId).Product);

            return RedirectToPage(new { returnUrl = returnUrl });
        }

    }
}
