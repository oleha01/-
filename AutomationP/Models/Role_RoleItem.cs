using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Role_RoleItem
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        virtual public Role Role { get; set; }
        public int RoleItemId { get; set; }
      virtual  public RoleItem RoleItem { get; set; }
    }
}
