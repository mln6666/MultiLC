using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiLC.Models
{
    public enum TipoPunto
    {
        [Display(Name = "Disponibles")]
        Disponibles = 1,

        [Display(Name = "No disponibles")]
        NoDisponibles = 0

        
    }
}