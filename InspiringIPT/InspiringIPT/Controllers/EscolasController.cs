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
    public class EscolasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Escolas
        public ActionResult Index()
        {
            return View(db.Escola.ToList());
        }

        // GET: Escolas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Escola escola = db.Escola.Find(id);
            if (escola == null)
            {
                return RedirectToAction("Index");
            }
            return View(escola);
        }

        // GET: Escolas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Escolas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult Create([Bind(Include = "EscolaID,NomeEscola,SiglaEscola")] Escola escola)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    TempData["createEscola"] = "Escola adicionado com sucesso!";
                    db.Escola.Add(escola);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Dados Incorretos");
            }
            

            return View(escola);
        }

        // GET: Escolas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Escola escola = db.Escola.Find(id);
            if (escola == null)
            {
                return RedirectToAction("Index");
            }
            return View(escola);
        }

        // POST: Escolas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult Edit([Bind(Include = "EscolaID,NomeEscola,SiglaEscola")] Escola escola)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    TempData["editEscola"] = "Dado alterado com sucesso!";
                    db.Entry(escola).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("","Dados Incorretos");
            }
            
            return View(escola);
        }

        // GET: Escolas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Escola escola = db.Escola.Find(id);
            if (escola == null)
            {
                return RedirectToAction("Index");
            }
            return View(escola);
        }

        // POST: Escolas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Escola escola = db.Escola.Find(id);

                TempData["createEscola"] = "Escola adicionado com sucesso!";
                db.Escola.Remove(escola);
                db.SaveChanges();
            }
            catch (Exception)
            {

                ModelState.AddModelError("", "Dados Incorretos");
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
