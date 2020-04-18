using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class User
    {
        public int ID { get; set; }
        [StringLength(20, MinimumLength = 5)]
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int Status { get; set; }
    }
}
