using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MultiLC1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Usuario empresa = new Usuario();
            //empresa.Nombre = "Lucas";
            //empresa.Apellido = "Correa";
            //empresa.Email = "lucascorrea@gmail.com";
            //empresa.Dni = "33333333";

            //Mesa mesa = new Mesa();
            //mesa.Usuarios.Add(empresa);
            //mesa.EstadoMesa = EstadoMesa.Activa;
            //mesa.FechaCreacion = DateTime.Now;


            //db.Mesas.Add(mesa);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
