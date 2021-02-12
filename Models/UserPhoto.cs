using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserPhoto
    {
        [Key]

        public int Id { get; set; }

        public byte[]  Photo { get; set; }

        public string ContentType { get; set; }

    }
}
