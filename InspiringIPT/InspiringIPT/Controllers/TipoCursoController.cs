using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InspiringIPT.Models;

namespace InspiringIPT.Controllers
{
    public class TipoCursoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TipoCurso
        public ActionResult Index()
        {
            return View(db.TipoCurso.ToList());
        }

        // GET: TipoCurso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            TipoCurso tipoCurso = db.TipoCurso.Find(id);
            if (tipoCurso == null)
            {
                return RedirectToAction("Index");
            }
            return View(tipoCurso);
        }

        // GET: TipoCurso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoCurso/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult Create([Bind(Include = "TipoID,Tipo")] TipoCurso tipoCurso)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    TempData["createTCurso"] = "Tipo de Curso criado com sucesso!";
                    db.TipoCurso.Add(tipoCurso);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                // não consigo guardar as alterações
                // No mínimo, preciso de 
                // notificar o utilizador que o processo falhou
                ModelState.AddModelError("","Dados incorrectos");
            }

          
            return View(tipoCurso);
        }

        // GET: TipoCurso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            TipoCurso tipoCurso = db.TipoCurso.Find(id);
            if (tipoCurso == null)
            {
                return RedirectToAction("Index");
            }
            return View(tipoCurso);
        }

        // POST: TipoCurso/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult Edit([Bind(Include = "TipoID,Tipo")] TipoCurso tipoCurso)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    TempData["editTCurso"] = "Dado alterado com sucesso!";
                    db.Entry(tipoCurso).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                // não consigo guardar as alterações
                // No mínimo, preciso de 
                // notificar o utilizador que o processo falhou
                ModelState.AddModelError("", "Dados incorrectos");
            }
     
            return View(tipoCurso);
        }

        // GET: TipoCurso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            TipoCurso tipoCurso = db.TipoCurso.Find(id);
            if (tipoCurso == null)
            {
                return RedirectToAction("Index");
            }
            return View(tipoCurso);
        }

        // POST: TipoCurso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                TipoCurso tipoCurso = db.TipoCurso.Find(id);
                TempData["createTCurso"] = "Tipo de Curso criado com sucesso!";
                db.TipoCurso.Remove(tipoCurso);
                db.SaveChanges();
            }
            catch (Exception)
            {
                // não consigo guardar as alterações
                // No mínimo, preciso de 
                // notificar o utilizador que o processo falhou
                ModelState.AddModelError("", "Dados incorrectos");
            }

           
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
