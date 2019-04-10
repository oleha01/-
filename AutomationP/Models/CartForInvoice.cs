using System.Collections.Generic;
using System.Linq;


namespace Library.Models
{
    public class CartForInvoice
    {
        private List<Invoice_Product> lineCollection = new List<Invoice_Product>();

        public void AddItem(Product product, int quantity)
        {
            Invoice_Product line = lineCollection
                .Where(g => g.Product.Id == product.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new Invoice_Product
                {
                    ProductId=product.Id,
                    Product=product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {

        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.SellingPrice * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<Invoice_Product> Lines
        {
            get { return lineCollection; }
        }
    }
}