using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Sales_Product
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        virtual public Sales Sale { get; set; }
        public int ProductId { get; set; }
        virtual public Product Product { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        public int? UserId { get; set; }
        virtual public User User { get; set; }
    }
}
