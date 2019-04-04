using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models
{
    public class Point_Storage
    {
        public int Id { get; set; }
        public int PointOfSaleId { get; set; }
        virtual public PointOfSale PointOfSale { get; set; }
        public int StorageId { get; set; }
        virtual public Storage Storage { get; set; }
    }
}
