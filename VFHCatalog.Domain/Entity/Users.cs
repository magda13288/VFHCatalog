using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalog.Domain.Common;

namespace VFHCatalog.Domain.Entity
{
    public class Users:BaseEntity
    {
        public int UserType { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string NIP { get; set; }
        public string ZipCode { get; set; }
        public string Town { get; set; }
    }
}
