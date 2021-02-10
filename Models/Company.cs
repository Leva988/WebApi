using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQL_API.Models
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<User> Users { get; set; }
        
    }
}
