using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiLC.Models
{
    public class Mesa
    {
        [Key]
        public int IdMesa { get; set; }

        public Mesa()
        {
            this.Usuarios = new HashSet<Usuario>();
        }

       
        public ICollection<Usuario> Usuarios { get; set; }
        //Máximo 7 usuarios+ventas por mesa
        public ICollection<Venta> Ventas { get; set; }

        public EstadoMesa EstadoMesa { get; set; }
        
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaCierre { get; set; }   
        
    }
}