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
    [Authorize]
    public class PotencialAlunoController : Controller
    {
        //onde tem todos os objetos referenciados a nossa bases de dados 
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PotencialAluno  
        [Authorize(Roles = "Gestores,Colaboradores")]
        public ActionResult Lista()
        {
            //se o utilizador for do tipo "Colaborador" ou do tipo "Gestor" mostra a lista
            var potencialAluno = db.PotencialAluno.OrderByDescending(p => p.DataInscricao);
            return View(potencialAluno.ToList());
        }

        // GET: PotencialAluno/Details/5
        [Authorize(Roles = "Gestores")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // ver como corrigir isto - aulas
            }
            PotencialAluno potencialAluno = db.PotencialAluno.Find(id);
            if (potencialAluno == null)
            {
                return HttpNotFound();// ver como corrigir isto - aulas
            }
            //ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
            //ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.CursosFK);
            //ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);
            return View(potencialAluno);
        }

        [AllowAnonymous]
        // GET: PotencialAluno/Create
        public ActionResult Create()
        {
            //ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea");
            ViewBag.Cursos = db.Cursos.OrderBy(c => c.EscolaFK).OrderBy(c => c.Areas.NomeArea).ToList();
            //ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo");
            return View();
        }

        // POST: PotencialAluno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "NomeCompleto,Email,Concelho,DataNascimento,Contacto,Genero,HabAcademicas")] PotencialAluno potencialAluno, int?[] ListaDeCursos)
        {
            // falta aqui muita coisa ainda
            try
            {
                // completar os dados do potencial aluno
                potencialAluno.CodigoIdentificacao = Guid.NewGuid().ToString();
                potencialAluno.DataInscricao = DateTime.Now;

                // será que escolheu algum curso?
                var listaCursos = new List<Cursos>(); // cria a lista de cursos que o potencial aluno escolheu
                // se a lista de cursos escolhidos pelo aluno não for nula
                // adicionam-se às suas preferências
                if (ListaDeCursos != null)
                    for (int i = 0; i < ListaDeCursos.Count(); i++)
                    {
                        Cursos curso = db.Cursos.Find(ListaDeCursos[i]);
                        listaCursos.Add(curso);
                    }
                potencialAluno.ListaCursos = listaCursos;

                // será que escolheu algum tipo de curso?
                // ....



                if (ModelState.IsValid)
                {  // adiciona o objeto 'Cursos' a base de dados
                    db.PotencialAluno.Add(potencialAluno);
                    //torna a definitiva a adição
                    db.SaveChanges();
                    return RedirectToAction("ConfirmaAluno", new { id = potencialAluno.CodigoIdentificacao });
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            //ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
            ViewBag.Cursos = db.Cursos.OrderBy(c => c.EscolaFK).OrderBy(c => c.Areas.NomeArea).ToList();
            //ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);

            return View(potencialAluno);
        }

        // GET: PotencialAluno/DetalhesAluno/5
        [AllowAnonymous]
        public ActionResult ConfirmaAluno(string codigo)
        {
            if (codigo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PotencialAluno potencialAluno = db.PotencialAluno.Where(a => a.CodigoIdentificacao.Equals(codigo)).FirstOrDefault();

            if (potencialAluno == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(potencialAluno);
        }





        // GET: PotencialAluno/Edit/5
        [AllowAnonymous]
        public ActionResult Edit(string codigo)
        {
            if (codigo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PotencialAluno potencialAluno = db.PotencialAluno.Where(a => a.CodigoIdentificacao.Equals(codigo)).FirstOrDefault();

            if (potencialAluno == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
            //ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.CursosFK);
            //ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);

            return View(potencialAluno);
        }

        // POST: PotencialAluno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Edit([Bind(Include = "AlunoID,NomeCompleto,Email,Concelho,DataNascimento,Contacto,Genero,HabAcademicas")] PotencialAluno potencialAluno)
        {

            // fazer aqui o equivalente ao feito no POST do Create...

            if (ModelState.IsValid)
            {
                db.Entry(potencialAluno).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("DetalhesAluno", new { id = potencialAluno.CodigoIdentificacao });
            }

            //ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
            //ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.CursosFK);
            //ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);

            return View(potencialAluno);
        }


        // GET: PotencialAluno/DetalhesAluno/5
        [AllowAnonymous]
        public ActionResult DetalhesAluno(string codigo)
        {
            if (codigo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PotencialAluno potencialAluno = db.PotencialAluno.Where(a => a.CodigoIdentificacao.Equals(codigo)).FirstOrDefault();

            if (potencialAluno == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
            //ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.CursosFK);
            //ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);

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
