using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiLC.Models
{
    public enum EstadoMesa
    {

        [Display(Name = "Finalizada")]
        Finalizada = 0,

        [Display(Name = "Activa")]
        Activa = 1,

        [Display(Name = "Suspendida")]
        Suspendida = 2,

        [Display(Name = "Deshabilitada")]
        Deshabilitada = 3


    }
}