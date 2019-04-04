using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VendorCode { get; set; }
        public string BarCode { get; set; }
        virtual public Category Category { get; set; }
        public string Units { get; set; }
        public string Description { get; set; }
        public int SellingPrice { get; set; }
        public Product()
        {

        }
        public Product(string Name, string VendorCode, string BarCode, Category Category, string Units, string Description, int SellingPrice)
        {
            //  this.Id = Id;
            this.Name = Name;
            this.VendorCode = VendorCode;
            this.BarCode = BarCode;
            this.Category = Category;
            this.Units = Units;
            this.Description = Description;
            this.SellingPrice = SellingPrice;
        }
    }
}
