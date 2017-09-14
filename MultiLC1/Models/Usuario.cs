using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiLC1.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        public Usuario()
        {
            this.Mesas = new HashSet<Mesa>();
        }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Dni { get; set; }

        public Usuario UsuarioPadre { get; set; }

        public EstadoUsuario EstadoUsuario { get; set; }

        public ICollection<Usuario> ListaReferidos { get; set; }


        public ICollection<Venta> Ventas { get; set; }

        public ICollection<Puntos> Puntos { get; set; }

        public virtual ICollection<Mesa> Mesas { get; set; }

    }
}