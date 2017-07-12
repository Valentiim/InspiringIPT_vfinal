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
    public class AreasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Areas
       
        public ActionResult Index()
        {
            return View(db.Areas.ToList());
        }

        // GET: Areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Areas areas = db.Areas.Find(id);
            if (areas == null)
            {
                return RedirectToAction("Index");
            }
            return View(areas);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult Create([Bind(Include = "AreaID,NomeArea")] Areas areas)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    TempData["createArea"] = "Área criado com sucesso!";
                    db.Areas.Add(areas);
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

            

            return View(areas);
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Areas areas = db.Areas.Find(id);
            if (areas == null)
            {
                return RedirectToAction("Index");
            }
            return View(areas);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult Edit([Bind(Include = "AreaID,NomeArea")] Areas areas)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    TempData["editArea"] = "Dados alterados com sucesso!";
                    db.Entry(areas).State = System.Data.Entity.EntityState.Modified;
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

            
            return View(areas);
        }

        // GET: Areas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Areas areas = db.Areas.Find(id);
            if (areas == null)
            {
                return RedirectToAction("Index");
            }
            return View(areas);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Areas areas = db.Areas.Find(id);
                TempData["deleteArea"] = "Dado apagado com sucesso!";
                db.Areas.Remove(areas);
                db.SaveChanges();
            }
            catch (Exception)
            {
                // não consigo guardar as alterações
                // No mínimo, preciso de 
                // notificar o utilizador que o processo falhou
                ModelState.AddModelError("","Dados incorrectos");
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
