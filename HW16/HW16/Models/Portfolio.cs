using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW16.Models
{
    public class Portfolio
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
