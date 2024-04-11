using BuildsByBrickwellNew.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuildsByBrickwellNew.Components
{
    public class ProductTypesViewComponent : ViewComponent
    {
        private IntexProjectContext _context;

        // constructor
        public ProductTypesViewComponent(IntexProjectContext temp)
        {
            _context = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedProductType = RouteData?.Values["productType"];
            var productTypes = _context.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(productTypes);
        }
    }
}
