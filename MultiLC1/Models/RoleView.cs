using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiLC1.Models
{
    public class RoleView
    {
        [Key]
        public string RoleID { get; set; }

        public string Name { get; set; }

    }
}