﻿using System;
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
        public ActionResult Index()
        {
            //cria uma lista com os dados dos Potenciais Alunos
            var potencialAluno = db.PotencialAluno.OrderByDescending(p => p.DataInscricao);

            /*solicita a execução da view
            fornecendo a lista dos Potenciais Alunos*/
            return View(potencialAluno.ToList());
        }

        // GET: PotencialAluno/Details/5
        [Authorize(Roles = "Gestores,Colaboradores")]
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

            ViewBag.Cursos = db.PotencialAluno.Find(id).ListaCursos;

            ViewBag.Areas = db.PotencialAluno.Find(id).ListaAreas;
            ViewBag.TipoCurso = db.PotencialAluno.Find(id).ListaTipoCurso;
            return View(potencialAluno);
        }


        // GET: PotencialAluno/Create
        /// <summary>
        /// mostra a View para possibilitar a criação de un novo 'potencial aluno'
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Create()
        {
            // procura dados necessários para o correto funcionamento da View
            ViewBag.Areas = db.Areas.OrderBy(a => a.NomeArea);
            ViewBag.Cursos = db.Cursos.OrderBy(c => c.EscolaFK).OrderBy(c => c.Areas.NomeArea).ToList();
            ViewBag.TipoCurso = db.TipoCurso.OrderBy(t => t.Tipo);

            // invoca a View
            return View();
        }

        // POST: PotencialAluno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Guarda os dados de um potencial aluno
        /// </summary>
        /// <param name="potencialAluno"></param>
        /// <param name="ListaDeCursos"></param>
        /// <param name="ListaDeAreas"></param>
        /// <param name="ListaDeTipoCurso"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "NomeCompleto,Email,Concelho,DataNascimento,Contacto,Genero,HabAcademicas")] PotencialAluno potencialAluno, int?[] ListaDeCursos, int?[] ListaDeAreas, int?[] ListaDeTipoCurso)
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
                if (ListaDeAreas != null)
                    for (int i = 0; i < ListaDeAreas.Count(); i++)
                    {
                        Areas area = db.Areas.Find(ListaDeAreas[i]);
                        listaAreas.Add(area);
                    }
                potencialAluno.ListaAreas = listaAreas;

                //**************************************************** Lista do Tipo de Cursos ************
                // será que escolheu algum tipo de curso?
                if (ListaDeTipoCurso != null)
                    for (int i = 0; i < ListaDeTipoCurso.Count(); i++)
                    {
                        TipoCurso tipocurso = db.TipoCurso.Find(ListaDeTipoCurso[i]);
                        listaTipoCurso.Add(tipocurso);
                    }
                potencialAluno.ListaTipoCurso = listaTipoCurso;


                if (ModelState.IsValid)
                {  // adiciona o objeto 'Cursos' a base de dados
    
                    db.PotencialAluno.Add(potencialAluno);
                    //torna a definitiva a adição
                    TempData["SubmitSucess"] = "Obrigado! O seu registo foi criado com sucesso!";
                    db.SaveChanges();

                    return RedirectToAction("ConfirmaAluno", new { id = potencialAluno.CodigoIdentificacao });
                }
            }
            catch (Exception)
            {
                // não consigo guardar as alterações
                // No mínimo, preciso de 
                // notificar o utilizador que o processo falhou
                ModelState.AddModelError("", "Dados incorrectos");
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

            return View(potencialAluno);
        }


        // GET: PotencialAluno/EditarAluno
        /// <summary>
        /// mostra a View para possibilitar a edição de um  'potencial aluno'
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult EditarAluno()
        {
            // invoca a View
            return View();
        }

        // GET: PotencialAluno/Edit/5
        [AllowAnonymous]
        public ActionResult Edit(string codigoAluno)
        {
            // avalia se o parâmetro é nulo
            if (codigoAluno == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PotencialAluno potencialAluno = db.PotencialAluno.Where(a => a.CodigoIdentificacao.Equals(codigoAluno)).FirstOrDefault();

            // avalia se o parâmetro é nulo
            if (potencialAluno == null)
            {
                // não esquecer enviar msg de erro
                // ver como foi feito nas outras views...
                // enviar o código errado
                return RedirectToAction("EditarAluno");
            }

            ViewBag.Areas = db.Areas.OrderBy(a => a.NomeArea).ToList();
            ViewBag.Cursos = db.Cursos.OrderBy(c => c.EscolaFK).OrderBy(c => c.Areas.NomeArea).ToList();
            ViewBag.TipoCurso = db.TipoCurso.OrderBy(t => t.Tipo).ToList();

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
                 
                    db.Entry(potencialAluno).State = System.Data.Entity.EntityState.Modified;
                    TempData["SubmitSucess"] = "Obrigado! O seu dado foi alterado com sucesso!";
                    db.SaveChanges();

                    return RedirectToAction("DetalhesAluno", new { id = potencialAluno.CodigoIdentificacao });
                }

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

            ViewBag.Areas = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.ListaAreas);
            ViewBag.Cursos = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.ListaCursos);
            ViewBag.TipoCurso = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.ListaTipoCurso);

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
