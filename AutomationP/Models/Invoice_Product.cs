using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Invoice_Product
    {
        public int Id { get; set; }
       // public int InvoiceId { get; set; }
       // public IncomingInvoice Invoice { get; set; }
        public int ProductId { get; set; }
       virtual public Product Product { get; set; }
        public int Capacity { get; set; }
    }
}
