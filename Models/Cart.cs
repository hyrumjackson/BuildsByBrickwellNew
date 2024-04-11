namespace BuildsByBrickwellNew.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Product p, int quantity)
        {
            CartLine? line = Lines
                .Where(x => x.Product.ProductId == p.ProductId)
                .FirstOrDefault();

            // Has this item already been added to our cart?
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Product = p,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product p) => Lines.RemoveAll(x => x.Product.ProductId == p.ProductId);

        public virtual void ClearCart() => Lines.Clear();

        public decimal CalculateTotal() => (decimal)Lines.Sum(x => x.Product.Price * x.Quantity);

        public class CartLine
        {
            public int CartLineId { get; set; }
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}
