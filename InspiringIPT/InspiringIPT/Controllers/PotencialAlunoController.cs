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
    //força a que só os utilizadores AUTENTICADOS consigam aceder aos metodos desta classe, aplica a todos os métodos
    //[Authorize]
    public class PotencialAlunoController : Controller
    {
        //onde tem todos os objetos referenciados a nossa bases de dados 
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PotencialAluno
        public ActionResult Lista()
        {
           
            // utilizadores de perfil 'Funcionario' ou
            // perfil 'Veterinário'
            if (User.IsInRole("Colaboradores") ||
              User.IsInRole("Gestores"))
            {
                var potencialAluno = db.PotencialAluno.Include(p => p.Area).Include(p => p.Curso).Include(p => p.TipoC).OrderByDescending(p => p.DataInscricao);
                return View(potencialAluno.ToList());

            }
           return View(db.PotencialAluno.
         Where(d => d.NomeCompleto == User.Identity.Name).
        ToList());
        }

        // GET: PotencialAluno/Details/5
        // [Authorize(Roles = "Gestores")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PotencialAluno potencialAluno = db.PotencialAluno.Find(id);
            if (potencialAluno == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
            ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.CursosFK);
            ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);
            return View(potencialAluno);
        }

        // GET: PotencialAluno/Create
        public ActionResult Create()
        {
            ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea");
            ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso");
            ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo");
            return View();
        }

        // POST: PotencialAluno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult Create([Bind(Include = "AlunoID,CursoID,AreaID,TipoID,NomeCompleto,Email,Concelho,DataNascimento,Contacto,Genero,DataInscricao,HabAcademicas,CursosFK,AreasFK,TiposCursosFK,")] PotencialAluno potencialAluno)
        {

            if (ModelState.IsValid)
            {  // adiciona o objeto 'Cursos' a base de dados
                db.PotencialAluno.Add(potencialAluno);
                //torna a definitiva a adição
                db.SaveChanges();
                return RedirectToAction("Lista");
            }

            ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
            ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.CursosFK);
            ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);
            return View(potencialAluno);
        }

        // GET: PotencialAluno/Edit/5
      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            PotencialAluno potencialAluno = db.PotencialAluno.Find(id);
            if (potencialAluno == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
            ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.CursosFK);
            ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);

            return View(potencialAluno);
        }

        // POST: PotencialAluno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult Edit([Bind(Include = "AlunoID,CursoID,AreaID,TipoID,NomeCompleto,Email,Concelho,DataNascimento,Contacto,Genero,DataInscricao,HabAcademicas,UserID,CursosFK,AreasFK,TiposCursosFK,UserID")] PotencialAluno potencialAluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(potencialAluno).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details", new { id = potencialAluno.AlunoID });
            }
            ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
            ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.CursosFK);
            ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);

            return View(potencialAluno);
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
