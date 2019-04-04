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
        public DateTime Date { get; set; }
    }
}
