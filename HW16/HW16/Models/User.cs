using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW16.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Portfolio> Portfolios { get; set; }
    }
}
