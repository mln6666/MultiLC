﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MultiLC1.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;



namespace MultiLC1.Controllers
{   
    public class UsersController : Controller
    {

        private MultiContext db = new MultiContext();
        // GET: Users
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var usersView = new List<UserView>();

            foreach (var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    Name = user.UserName,
                    UserID = user.Id
                };
                usersView.Add(userView);
            }
            return View(usersView);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult VerPermisos()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in roles)
            {

                var roleView = new RoleView
                {
                    RoleID = item.Id,
                    Name = item.Name
                };
                rolesView.Add(roleView);
            }
            var userView = new UserView
            {
                Roles = rolesView.OrderBy(name => name.Name).ToList()
            };
            ViewBag.dadministrador = "Superusuario del sistema. Todas las funciones habilitadas.";
            ViewBag.dmoderador = "Moderador del sistema.";
            ViewBag.dinvitado = "Sólo posee permisos de lectura";
            return View(userView);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Roles(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }
            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView.OrderBy(name => name.Name).ToList()
            };

            ViewBag.dadministrador = "Superusuario del sistema. Todas las funciones habilitadas.";
            ViewBag.dmoderador = "Moderador del sistema.";
            ViewBag.dinvitado = "Sólo posee permisos de lectura";
           

            return View(userView);
        }

        public ActionResult MisRoles(string name1)
        {
            if (string.IsNullOrEmpty(name1))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();

            var user = users.Find(u => u.UserName == name1);

            if (user == null)
            {
                return HttpNotFound();
            }

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                var role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }


            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView.OrderBy(name => name.Name).ToList()
            };

            ViewBag.dadministrador = "Superusuario del sistema. Todas las funciones habilitadas.";
            ViewBag.dmoderador = "Moderador del sistema.";
            ViewBag.dinvitado = "Sólo posee permisos de lectura";
            

            return View("MisPermisos", userView);
        }

        //get
        [Authorize(Roles = "Administrador")]
        public ActionResult AddRole(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id
            };

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var list = roleManager.Roles.ToList();
            list.Remove(list.Single(x => x.Name == "Administrador"));
            //list.Add(new IdentityRole { Id = "", Name = "[Seleccione un permiso...]" });
            list = list.OrderBy(r => r.Name).ToList();

            ViewBag.permisos = list;
            ViewBag.RoleID = new SelectList(list, "Id", "Name");




            return View(userView);
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public ActionResult AddRole(string userID, FormCollection form)
        {
            var roleID = Request["RoleID"];
            ViewBag.dadministrador = "Superusuario del sistema. Todas las funciones habilitadas.";
            ViewBag.dmoderador = "Moderador del sistema.";
            ViewBag.dinvitado = "Sólo posee permisos de lectura";


            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(u => u.Id == userID);
            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id
            };

            if (string.IsNullOrEmpty(roleID))
            {
                ViewBag.Error = "Debe Seleccionar un permiso";

                var list = roleManager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = "[Seleccione un permiso...]" });
                list = list.OrderBy(r => r.Name).ToList();
                ViewBag.RoleID = new SelectList(list, "Id", "Name");

                return View(userView);
            }

            var roles = roleManager.Roles.ToList();
            var role = roles.Find(r => r.Id == roleID);
            if (!userManager.IsInRole(userID, role.Name))
            {
                userManager.AddToRole(userID, role.Name);
                db.SaveChanges();
            }

            var rolesView = new List<RoleView>();
            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    Name = role.Name,
                    RoleID = role.Id
                };
                rolesView.Add(roleView);
            }


            userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                Roles = rolesView,
                UserID = user.Id
            };

            return View("Roles", userView);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = userManager.Users.ToList().Find(u => u.Id == userID);
            var role = roleManager.Roles.ToList().Find(r => r.Id == roleID);

            //Delete
            if (userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
                db.SaveChanges();
            }

            //Prepara la vista de vuelta
            var users = userManager.Users.ToList();
            var roles = roleManager.Roles.ToList();
            var rolesView = new List<RoleView>();

            foreach (var item in user.Roles)
            {
                role = roles.Find(r => r.Id == item.RoleId);
                var roleView = new RoleView
                {
                    RoleID = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }


            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserID = user.Id,
                Roles = rolesView
            };

            return View("Roles", userView);
        }

        // POST: /Users/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.Users.ToList().Find(u => u.Id == id);
            await userManager.DeleteAsync(user);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}