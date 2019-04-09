using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class IncomingInvoice
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int StorageId { get; set; }
        virtual public Storage Storage { get; set; }
        virtual public List<Invoice_Product> Invoice_Products { get; set; }
        public int UserId { get; set; }
        virtual public User User { get; set; }

    }
}
