﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Library.Models
{
    public class Sales
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int PointOfSaleId { get; set; }
        virtual public PointOfSale PointOfSale { get; set; }
        public int UserId { get; set; }
      virtual  public User User { get; set; }
        virtual public List<Sales_Product> Products { get; set; }
    }
}
