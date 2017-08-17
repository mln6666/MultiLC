using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Configuration;

namespace MultiLC.Models
{
    public class Puntos
    {
        [Key]
        public int IdPuntos { get; set; }

        public int Cantidad { get; set; }

        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public DateTime FechaAsignados { get; set; }

        public TipoPunto TipoPunto { get; set; }
    }
}