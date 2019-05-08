using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class MoneyAndSales
    {
        public DateTime Date { get; set; }
        public User User { get; set; }
        public PointOfSale PointOfSale { get; set; }
        public string Prychyna { get; set; }
        public int Price { get; set; }
        public string Coment { get; set; }
    }
}
