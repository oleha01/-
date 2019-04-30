using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Library.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        virtual public Product Product { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public int PointOfSaleId { get; set; }
        virtual public PointOfSale PointOfSale { get; set; }
    }
}
