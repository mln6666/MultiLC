using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiLC.Models
{
    public class Venta
    {
        [Key]
        public int IdVenta { get; set; }

        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public string Descripcion { get; set; }

    }
}