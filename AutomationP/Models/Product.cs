using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace Library.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Не вказано ім'я")]
        public string Name { get; set; }
        public string VendorCode { get; set; }
        public string BarCode { get; set; }
        public int ParCategoryId { get; set; }
        [JsonIgnore]
        virtual public Category ParCategory { get; set; }
        public string Units { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Не вказана ціна")]
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
            this.ParCategory = Category;
            this.Units = Units;
            this.Description = Description;
            this.SellingPrice = SellingPrice;
        }

    }
}
