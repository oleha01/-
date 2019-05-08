using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace Library.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не вказано ім'я")]
    public string Name { get; set; }
        public Category()
        {
                
        }
        public Category(string Name)
        {
            this.Name = Name;
        }
        
        public int? ParentCategoryId { get; set; }
        
        
        virtual  public Category ParentCategory { get; set; }
        public int EnterpriseId { get; set; }
        virtual public Enterprise Enterprise { get; set; }
        virtual public List<Product> Products { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
