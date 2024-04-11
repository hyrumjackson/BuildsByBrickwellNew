namespace BuildsByBrickwellNew.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public IQueryable<Product> Colors { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
        public string? CurrentProductType { get; set; }
        public string? CurrentProductColor { get; set; }
    }
}
