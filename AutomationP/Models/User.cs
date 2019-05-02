using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;


namespace Library.Models
{
    public class User
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int EnterpriseId { get; set; }
        virtual public Enterprise Enterprise { get; set; }
        virtual public List<Role> Roles { get; set; }
    }
}
