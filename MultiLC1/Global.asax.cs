using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MultiLC1.Models;

namespace MultiLC1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
          
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MultiContext db = new MultiContext();
            if (db.Users == null || db.Users.Count() == 0)
            {
                CreateRoles(db);
                CreateSuperuser(db);
                AddPermisionsToSuperuser(db);
            }
            //if (db.Usuarios == null || db.Usuarios.Count() == 0)
            //{
            //    if (db.Mesas==null || db.Mesas.Count()==0) { }
            //    Usuario empresa = new Usuario();
            //    empresa.Nombre = "EmpresaNombre";
            //    empresa.Apellido = "EmpresaApellido";
            //    empresa.Email = "empresa@empresa.com";
            //    empresa.Dni = "33333333";
            //    db.Usuarios.Add(empresa);
            //    db.SaveChanges();

            //    Mesa mesa = new Mesa();
            //    mesa.Usuarios.Add(empresa);
            //    mesa.EstadoMesa = EstadoMesa.Activa;
            //    //mesa.FechaCreacion = DateTime.Now;
            //    db.Mesas.Add(mesa);
            //    db.SaveChanges();
            //}

        }
        private void AddPermisionsToSuperuser(MultiContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));


            var user = userManager.FindByName("admin@multilc.com");

            //if (!userManager.IsInRole(user.Id, "Moderador"))
            //{
            //    userManager.AddToRole(user.Id, "Moderador");
            //}


            if (!userManager.IsInRole(user.Id, "Administrador"))
            {
                userManager.AddToRole(user.Id, "Administrador");
            }
            //if (!userManager.IsInRole(user.Id, "Invitado"))
            //{
            //    userManager.AddToRole(user.Id, "Invitado");
            //}

        }

        private void CreateSuperuser(MultiContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("admin@multilc.com");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "admin@multilc.com",
                    Email = "admin@multilc.com"
                };
                userManager.Create(user, "Abc123.");
            }

        }
        private void CreateRoles(MultiContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("Administrador"))
            {
                roleManager.Create(new IdentityRole("Administrador"));
            }

            if (!roleManager.RoleExists("Moderador"))
            {
                roleManager.Create(new IdentityRole("Moderador"));
            }
            if (!roleManager.RoleExists("Invitado"))
            {
                roleManager.Create(new IdentityRole("Invitado"));
            }


        }

    }
}
