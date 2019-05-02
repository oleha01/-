using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class User_Role
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        virtual public User User { get; set; }
        public int RoleId { get; set; }
        virtual public Role Role { get; set; }
    }
}