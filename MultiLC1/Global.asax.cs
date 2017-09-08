using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MultiLC.Context;
using MultiLC.Models;

namespace MultiLC1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MultiContext db = new MultiContext();
          
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (db.Usuarios == null || db.Usuarios.Count() == 0)
            {
                if (db.Mesas==null || db.Mesas.Count()==0) { }
                Usuario empresa = new Usuario();
                empresa.Nombre = "EmpresaNombre";
                empresa.Apellido = "EmpresaApellido";
                empresa.Email = "empresa@empresa.com";
                empresa.Dni = "33333333";
                db.Usuarios.Add(empresa);
                db.SaveChanges();

                Mesa mesa = new Mesa();
                mesa.Usuarios.Add(empresa);
                mesa.EstadoMesa = EstadoMesa.Activa;
                mesa.FechaCreacion = DateTime.Now;
                db.Mesas.Add(mesa);
                db.SaveChanges();
            }

        }
    }
}
