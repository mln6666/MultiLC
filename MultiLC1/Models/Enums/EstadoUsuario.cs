using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiLC.Models
{
    public enum EstadoUsuario
    {
        [Display(Name = "Inactivo")]
        Inactivo = 0,

        [Display(Name = "Activo")]
        Activo = 1
    }
}