using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Enterprise
    {
        public int Id { get; set; }
        public string Name { get; set; }
       virtual public List<Category> Categories { get; set; }
        virtual public List<PointOfSale> PointOfSales { get; set; }
        virtual public List<Role> Roles { get; set; }

    }
}
