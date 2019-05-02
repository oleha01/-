using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class User_Point
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        virtual public User User { get; set; }
        public int PointId { get; set; }
        virtual public PointOfSale Point { get; set; }
    }
}