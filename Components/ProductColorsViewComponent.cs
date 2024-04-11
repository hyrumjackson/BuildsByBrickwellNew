using BuildsByBrickwellNew.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuildsByBrickwellNew.Components
{
    public class ProductColorsViewComponent : ViewComponent
    {
        private IntexProjectContext _context;

        // constructor
        public ProductColorsViewComponent(IntexProjectContext temp)
        {
            _context = temp;
        }
        public IViewComponentResult Invoke()
        {
            var productColor = HttpContext.Request.Query["productColor"];
            ViewBag.SelectedProductColor = productColor;

            var productColors = _context.Products
                .Select(x => x.PrimaryColor)
                .Distinct()
                .OrderBy(x => x);

            return View(productColors);
        }
    }
}
