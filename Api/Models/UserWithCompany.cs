using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    [NotMapped]
    public class UserWithCompany: User
    {
        public string Company { get; set; }
        
    }
}
