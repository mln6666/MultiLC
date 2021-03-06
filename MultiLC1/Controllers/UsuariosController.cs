﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MultiLC1.Models;

namespace MultiLC1.Controllers
{
    public class UsuariosController : Controller
    {
        private MultiContext db = new MultiContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            return View(db.Usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,Nombre,Apellido,Email,Dni,EstadoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }
        
        [HttpPost]
        public JsonResult AddUser(Usuario user)
        {
            var exist = false;
            var newid = new int();
            if (user.Dni != "" & user.Dni != null)
            {
                exist = db.Usuarios.ToList().Exists(u => u.Dni == user.Dni);
            }
            if (user.Nombre != null & user.Apellido != null & !exist)
            {
                Usuario usuario = new Usuario();
                usuario.Nombre = user.Nombre;
                usuario.Apellido = user.Apellido;
                usuario.Email = user.Email;
                usuario.Dni = user.Dni;
                usuario.EstadoUsuario = user.EstadoUsuario;
                db.Usuarios.Add(usuario);
                db.SaveChanges();

                newid = db.Usuarios.ToList().Last().IdUsuario;
            }
            return new JsonResult { Data = new { status = !exist, newuserid = newid } };
        }

        [HttpPost]
        public JsonResult EditUser(Usuario user)
        {
            var exist = false;
            if (user.Dni != "" & user.Dni != null)
            {
                exist = db.Usuarios.ToList().Exists(u => u.Dni == user.Dni & u.IdUsuario != user.IdUsuario);
            }
            if (user.Nombre != null & user.Apellido != null & !exist)
            {
                Usuario usuario = db.Usuarios.Find(user.IdUsuario);
                if (usuario != null)
                {
                    usuario.Nombre = user.Nombre;
                    usuario.Apellido = user.Apellido;
                    usuario.Email = user.Email;
                    usuario.Dni = user.Dni;

                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return new JsonResult { Data = new { status = !exist} };
        }

        [HttpPost]
        public JsonResult DeleteUser(int userid)
        {
            var ok = false;

            Usuario user = db.Usuarios.Find(userid);

            if (user != null)
            {
                if (!user.Mesas.Any())
                {
                    db.Usuarios.Remove(user);
                    db.SaveChanges();
                    ok = true;
                }
            }

            return new JsonResult { Data = new { status = ok } };
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,Nombre,Apellido,Email,Dni,EstadoUsuario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
