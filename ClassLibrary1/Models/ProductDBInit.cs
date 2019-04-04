using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClassLibrary.Models
{
    public class ProductDBInitializer : DropCreateDatabaseAlways<ProductContext>
    {
        protected override void Seed(ProductContext db)
        {
            Category category = new Category("Flash Drives");
            db.Products.Add(new Product("Flash Drive", "00", "123456", category, "sht.", "Flash 16GB", 200));
            db.Products.Add(new Product("Flash Drive", "01", "12345678", category, "sht.", "Flash 32GB", 400));
            db.Products.Add(new Product("Flash Drive", "02", "99999", category, "sht.", "Flash 61GB", 600));


            base.Seed(db);
        }
    }
}
