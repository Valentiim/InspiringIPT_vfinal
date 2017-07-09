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
    [Authorize] //força a que só os utilizadores AUTENTICADOS consigam aceder aos metodos desta classe, aplica a todos os métodos
    public class CursosController : Controller
    {
        //Objeto que referencia a nossa bases de dados "db"
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cursos
        [AllowAnonymous] //permite o acesso de UTILIZADORES ANÓNIMOS aos conteúdos deste método
        public ActionResult Index()
        {
            ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea");
            ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo");
            ViewBag.EscolasFK = new SelectList(db.Escola, "EscolaID", "NomeEscola");
            return View(db.Cursos.OrderByDescending(m=> m.NomeCurso).ToList());

        }


        // GET: Cursos/Create
        public ActionResult Create()
        {
            ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea");
            ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo");
            ViewBag.EscolasFK = new SelectList(db.Escola, "EscolaID", "NomeEscola");
            return View();
        }

        // POST: Cursos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult Create([Bind(Include = "CursoID,AreaID,TipoID,EscolaID,NomeCurso,SiglaCurso,Descricao,AreaFK,TipoCursoFK,EscolaFK")] Cursos cursos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    TempData["createCurso"] = "Novo Curso adicionado com sucesso!";
                    // adiciona o objeto 'Cursos' a base de dados
                    db.Cursos.Add(cursos);
                    //torna a definitiva a adição
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                // não consigo guardar as alterações
                // No mínimo, preciso de 
                // notificar o utilizador que o processo falhou
                ModelState.AddModelError("","Dados incorectos!");
            }
            
            ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", cursos.AreaFK);
            ViewBag.EscolasFK = new SelectList(db.Escola, "EscolaID", "NomeEscola", cursos.EscolaFK);
            ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", cursos.TipoCursoFK);

            return View(cursos);
        }

        // GET: Cursos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Cursos cursos = db.Cursos.Find(id);
            if (cursos == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea");
            ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo");
            ViewBag.EscolasFK = new SelectList(db.Escola, "EscolaID", "NomeEscola");
            return View(cursos);
        }

        // POST: Cursos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Gestores")]
        public ActionResult Edit([Bind(Include = "CursoID,NomeCurso,SiglaCurso,Descricao,AreaFK,TipoCursoFK,EscolaFK")] Cursos cursos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TempData["editCurso"] = "Dado alterado com sucesso!";
                    db.Entry(cursos).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                // não consigo guardar as alterações
                // No mínimo, preciso de 
                // notificar o utilizador que o processo falhou
                ModelState.AddModelError("", "Dados incorectos!");
            }

           
            ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", cursos.AreaFK);
            ViewBag.EscolasFK = new SelectList(db.Escola, "EscolaID", "NomeEscola", cursos.EscolaFK);
            ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", cursos.TipoCursoFK);

            return View(cursos);
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
