using System;
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
    public class MesasController : Controller
    {
        private MultiContext db = new MultiContext();

        // GET: Mesas
        public ActionResult Index()
        {
            
            return View(db.Mesas.ToList());
        }

        // AGREGAR USUARIO A MESA.
        public ActionResult AgregarUsuario(int? idusuario)
        {
            if (idusuario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(idusuario);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            //si esta activo se asigna a la mesa del usuario que le sugirio(padre/sugerido ver bien esto)
            if (usuario.UsuarioPadre.EstadoUsuario == EstadoUsuario.Activo)
                {
                    Mesa mesa = usuario.UsuarioPadre.Mesas.LastOrDefault();
                    //actualizo el padre
                   usuario= ActualizarPadre(usuario, mesa);

                    mesa.Usuarios.Add(usuario);
                    db.Entry(mesa).State = EntityState.Modified;
                    db.SaveChanges();
                //HAY QUE TRATAR ACA QUE PASA CON LA MESA CUANDO SE LLENA
            }
            //si esta inactivo se va para la EMPRESA
            else if (usuario.UsuarioPadre.EstadoUsuario == EstadoUsuario.Inactivo)
            {
                Mesa mesa = db.Usuarios.FirstOrDefault().Mesas.LastOrDefault();
                //actualizo el padre
                usuario = ActualizarPadre(usuario, mesa);

                mesa.Usuarios.Add(usuario);
                db.Entry(mesa).State = EntityState.Modified;
                db.SaveChanges();
                //HAY QUE TRATAR ACA QUE PASA CON LA MESA CUANDO SE LLENA

            }


            return View(mesa);
        }


        public Usuario ActualizarPadre(Usuario usuario, Mesa mesa)
        {
            int pos = mesa.Usuarios.Count() + 1;

            switch (pos)
            {
                case 2:
                    usuario.UsuarioPadre = mesa.Usuarios.ElementAt(0);
                    break;
                case 3:
                    usuario.UsuarioPadre = mesa.Usuarios.ElementAt(0);
                    break;
                case 4:
                    usuario.UsuarioPadre = mesa.Usuarios.ElementAt(1);
                    break;
                case 5:
                    usuario.UsuarioPadre = mesa.Usuarios.ElementAt(1);
                    break;
                case 6:
                    usuario.UsuarioPadre = mesa.Usuarios.ElementAt(2);
                    break;
                case 7:
                    usuario.UsuarioPadre = mesa.Usuarios.ElementAt(2);
                    break;
            }
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();


            return usuario;
        }

        // GET: Mesas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesa mesa = db.Mesas.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // GET: Mesas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mesas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdMesa,EstadoMesa,FechaCreacion,FechaCierre")] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                db.Mesas.Add(mesa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mesa);
        }

        // GET: Mesas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesa mesa = db.Mesas.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // POST: Mesas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdMesa,EstadoMesa,FechaCreacion,FechaCierre")] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mesa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mesa);
        }

        // GET: Mesas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mesa mesa = db.Mesas.Find(id);
            if (mesa == null)
            {
                return HttpNotFound();
            }
            return View(mesa);
        }

        // POST: Mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mesa mesa = db.Mesas.Find(id);
            db.Mesas.Remove(mesa);
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
