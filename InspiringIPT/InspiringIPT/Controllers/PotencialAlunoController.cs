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
            //cria uma lista com os dados dos Potenciais Alunos
            var potencialAluno = db.PotencialAluno.OrderByDescending(p => p.DataInscricao);

            /*solicita a execução da view
            fornecendo a lista dos Potenciais Alunos*/
            return View(potencialAluno.ToList());
        }

        // GET: PotencialAluno/Details/5
        [Authorize(Roles = "Gestores")]
        public ActionResult Details(int? id)
        {
            // avalia se o parâmetro é nulo
            if (id == null)
            {
                //redirecciona para a pagina de inicio
                return RedirectToAction("Index", "Home");

            }

            //procurar pelo Potencial Aluno, cujo o ID foi fornecido
            PotencialAluno potencialAluno = db.PotencialAluno.Find(id);

            //se o Potencial Aluno não existe a página não é encontrada
            if (potencialAluno == null)
            {
                //redirecciona para a pagina de inicio
                return RedirectToAction("Index", "Home");
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
            ViewBag.Areas = db.Areas.OrderBy(a => a.NomeArea);
            ViewBag.Cursos = db.Cursos.OrderBy(c => c.EscolaFK).OrderBy(c => c.Areas.NomeArea).ToList();
            ViewBag.TipoCurso = db.TipoCurso.OrderBy(t => t.Tipo);
            return View();
        }

        // POST: PotencialAluno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "NomeCompleto,Email,Concelho,DataNascimento,Contacto,Genero,HabAcademicas")] PotencialAluno potencialAluno, int?[] ListaDeCursos, int?[] ListaDeAreas, int?[] ListaDeTipoCurso )
        {

            int novoID = 0;
            try
            {
                novoID = db.PotencialAluno.Max(d => d.AlunoID) + 1;
                potencialAluno.AlunoID = novoID;
            }
            catch (System.Exception)
            {
                // a tabela 'PotencialAluno' está vazia
                // não sendo possível devolver o MAX de uma tabela vazia
                // Por isso,
                // vou atribuir 'manualmente' o valor do 'novoID'
                novoID = 1;
            }

            // falta aqui muita coisa ainda
            try
            {
                // completar os dados do potencial aluno
                potencialAluno.CodigoIdentificacao = Guid.NewGuid().ToString();
                potencialAluno.DataInscricao = DateTime.Now;

                // será que escolheu algum curso?
                var listaCursos = new List<Cursos>();// cria a lista de cursos que o potencial aluno escolheu
                var listaAreas = new List<Areas>(); //cria a lista de áreas que o potencial aluno escolheu
                var listaTipoCurso = new List<TipoCurso>(); //cria a lista de tipos de cursos que potencial aluno escolheu

                // se a lista de cursos escolhidos pelo aluno não for nula
                // adicionam-se às suas preferências
                if (ListaDeCursos != null)
                    for (int i = 0; i < ListaDeCursos.Count(); i++)
                    {
                        Cursos curso = db.Cursos.Find(ListaDeCursos[i]);
                        listaCursos.Add(curso);
                    }
                potencialAluno.ListaCursos = listaCursos;
                //**************************************************** Lista de Áreas *******************
                if (ListaDeAreas !=null)
                    for (int i = 0; i < ListaDeAreas.Count(); i++)
                    {
                        Areas area = db.Areas.Find(ListaDeAreas[i]);
                        listaAreas.Add(area);
                    }
                potencialAluno.ListaAreas = listaAreas;

                //**************************************************** Lista do Tipo de Cursos ************
                if (ListaDeTipoCurso != null)
                    for (int i = 0; i < ListaDeTipoCurso.Count(); i++)
                    {
                        TipoCurso tipocurso = db.TipoCurso.Find(ListaDeTipoCurso[i]);
                        listaTipoCurso.Add(tipocurso);
                    }
                potencialAluno.ListaTipoCurso = listaTipoCurso;


                // será que escolheu algum tipo de curso?
                // ....



                if (ModelState.IsValid)
                {  // adiciona o objeto 'Cursos' a base de dados

                    TempData["SubmitSucess"] = "Obrigado! O seu registo foi criado com sucesso!";
                    db.PotencialAluno.Add(potencialAluno);
                    //torna a definitiva a adição
                    db.SaveChanges();
                    return RedirectToAction("ConfirmaAluno", new { id = potencialAluno.CodigoIdentificacao });
                }
            }
            catch (Exception)
            {
                // não consigo guardar as alterações
                // No mínimo, preciso de 
                // notificar o utilizador que o processo falhou
                ModelState.AddModelError("","Dados incorrectos");
            }

            ViewBag.Areas = db.Areas.OrderBy(a => a.NomeArea).ToList();
            ViewBag.Cursos = db.Cursos.OrderBy(c => c.EscolaFK).OrderBy(c => c.Areas.NomeArea).ToList();
            ViewBag.TipoCurso = db.TipoCurso.OrderBy(t => t.Tipo).ToList();

            return View(potencialAluno);
        }

        // GET: PotencialAluno/DetalhesAluno/5
        [AllowAnonymous]
        public ActionResult ConfirmaAluno(string codigo)
        {
            // avalia se o parâmetro é nulo
            if (codigo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PotencialAluno potencialAluno = db.PotencialAluno.Where(a => a.CodigoIdentificacao.Equals(codigo)).FirstOrDefault();

            if (potencialAluno == null)
            {
                return RedirectToAction("Index", "Home");
            }

            TempData["codSucess"] = "Confirma o Potencial Aluno!";
            return View(potencialAluno);
        }

        // GET: PotencialAluno/Edit/5
        [AllowAnonymous]
        public ActionResult Edit(string codigo)
        {
            // avalia se o parâmetro é nulo
            if (codigo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PotencialAluno potencialAluno = db.PotencialAluno.Where(a => a.CodigoIdentificacao.Equals(codigo)).FirstOrDefault();

            // avalia se o parâmetro é nulo
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
        public ActionResult Edit([Bind(Include = "AlunoID,NomeCompleto,Email,Concelho,DataNascimento,Contacto,Genero,HabAcademicas")] PotencialAluno potencialAluno, int?[] ListaDeCursos, int?[] ListaDeAreas, int?[] ListaDeTipoCurso)
        {

            // fazer aqui o equivalente ao feito no POST do Create...


            int novoID = 0;
            try
            {
                
                novoID = db.PotencialAluno.Max(d => d.AlunoID) + 1;
                // select max(d.DonoID)
                // from donos d
            }
            catch (System.Exception)
            {
                // a tabela 'Donos' está vazia
                // não sendo possível devolver o MAX de uma tabela vazia
                // Por isso,
                // vou atribuir 'manualmente' o valor do 'novoID'
                novoID = 1;
            }

            try
            {
                // completar os dados do potencial aluno
                potencialAluno.CodigoIdentificacao = Guid.NewGuid().ToString();
                potencialAluno.DataInscricao = DateTime.Now;

                // será que escolheu algum curso?
                var listaCursos = new List<Cursos>();// cria a lista de cursos que o potencial aluno escolheu
                var listaAreas = new List<Areas>(); //cria a lista de áreas que o potencial aluno escolheu
                var listaTipoCurso = new List<TipoCurso>(); //cria a lista de tipos de cursos que potencial aluno escolheu

                // se a lista de cursos escolhidos pelo aluno não for nula
                // adicionam-se às suas preferências
                if (ListaDeCursos != null)
                    for (int i = 0; i < ListaDeCursos.Count(); i++)
                    {
                        Cursos curso = db.Cursos.Find(ListaDeCursos[i]);
                        listaCursos.Add(curso);
                    }
                potencialAluno.ListaCursos = listaCursos;
                //**************************************************** Lista de Áreas *******************
                if (ListaDeAreas != null)
                    for (int i = 0; i < ListaDeAreas.Count(); i++)
                    {
                        Areas area = db.Areas.Find(ListaDeAreas[i]);
                        listaAreas.Add(area);
                    }
                potencialAluno.ListaAreas = listaAreas;

                //**************************************************** Lista do Tipo de Cursos ************
                if (ListaDeTipoCurso != null)
                    for (int i = 0; i < ListaDeTipoCurso.Count(); i++)
                    {
                        TipoCurso tipocurso = db.TipoCurso.Find(ListaDeTipoCurso[i]);
                        listaTipoCurso.Add(tipocurso);
                    }
                potencialAluno.ListaTipoCurso = listaTipoCurso;


                // será que escolheu algum tipo de curso?
                // ....

                TempData["ad"] = "Alterar Dados";
                if (ModelState.IsValid)
                {
                    TempData["SubmitSucess"] = "Obrigado! O seu dado foi alterado com sucesso!";
                    db.Entry(potencialAluno).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("DetalhesAluno", new { id = potencialAluno.CodigoIdentificacao });
                }

                //ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
                //ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.CursosFK);
                //ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);
            }
            

            catch (System.Exception)
            {
                // não consigo guardar as alterações
                // No mínimo, preciso de 
                // notificar o utilizador que o processo falhou
                ModelState.AddModelError("", "Dados incorrectos");
            }

           
            return View(potencialAluno);
            }


        // GET: PotencialAluno/DetalhesAluno/5
        [AllowAnonymous]
        public ActionResult DetalhesAluno(string codigo)
        {
            // avalia se o parâmetro é nulo
            if (codigo == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PotencialAluno potencialAluno = db.PotencialAluno.Where(a => a.CodigoIdentificacao.Equals(codigo)).FirstOrDefault();

            // avalia se o parâmetro é nulo
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
