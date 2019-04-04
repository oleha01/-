using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Library.Models
{
    public class Storage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxWeight { get; set; }
        public int EnterpriseId { get; set; }
        virtual public Enterprise Enterprise { get; set; }
      virtual  public List<IncomingInvoice> IncomingInvoices { get; set; }
    }
}
