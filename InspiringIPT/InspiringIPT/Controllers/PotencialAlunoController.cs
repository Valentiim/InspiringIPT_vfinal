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
            // vamos reapresentar a View ao Potencial Aluno
            // enumera a lista de opções escolhidas peo potencial aluno
            // como é a 1ª vex, essa lista é vazia
            ViewBag.AuxAreas = "";
            ViewBag.AuxCursos = "";
            ViewBag.AuxTiposCurso = "";

            // procura dados necessários para o correto funcionamento da View
            ViewBag.Areas = db.Areas.OrderBy(a => a.NomeArea).ToList();
            ViewBag.Cursos = db.Cursos.OrderBy(c => c.EscolaFK).OrderBy(c => c.Areas.NomeArea).ToList();
            ViewBag.TipoCurso = db.TipoCurso.OrderBy(t => t.Tipo).ToList();

            // invoca a View
            return View();
        }



        // POST: PotencialAluno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Guarda os dados de um potencial aluno
        /// </summary>
        /// <param name="potencialAluno">objeto com os dados do aluno</param>
        /// <param name="ListaDeCursos">lista de Cursos escolhidos pelo aluno</param>
        /// <param name="ListaDeAreas">lista de Áreas escolhidas pelo aluno</param>
        /// <param name="ListaDeTipoCurso">lista de Tipos de cursos escolhido pelo aluno</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Create([Bind(Include = "NomeCompleto,Email,Concelho,DataNascimento,Contacto,Genero,HabAcademicas")] PotencialAluno potencialAluno, int?[] ListaDeCursos, int?[] ListaDeAreas, int?[] ListaDeTipoCurso)
        {

            // vars. auxiliares a serem utilizadas caso haja algum erro, e 
            // tenha se ser reapresentado o formulário ao potencial aluno
            string auxCursos = "";
            string auxTipoCursos = "";
            string auxAreas = "";

            try
            {
                // completar os dados do potencial aluno
                potencialAluno.CodigoIdentificacao = Guid.NewGuid().ToString();
                potencialAluno.DataInscricao = DateTime.Now;

                // será que escolheu algum curso?
                var listaCursos = new List<Cursos>();// cria a lista de cursos que o potencial aluno escolheu
                var listaAreas = new List<Areas>(); //cria a lista de áreas que o potencial aluno escolheu
                var listaTipoCurso = new List<TipoCurso>(); //cria a lista de tipos de cursos que potencial aluno escolheu

                //**************************************************** Lista de Cursos *********************
                // se a lista de cursos escolhidos pelo aluno não for nula
                // adicionam-se às suas preferências
                if (ListaDeCursos != null)
                    for (int i = 0; i < ListaDeCursos.Count(); i++)
                    {
                        Cursos curso = db.Cursos.Find(ListaDeCursos[i]);
                        listaCursos.Add(curso);
                        auxCursos += curso.CursoID + " ";
                    }
                potencialAluno.ListaCursos = listaCursos;

                //**************************************************** Lista de Áreas *********************
                if (ListaDeAreas != null)
                    for (int i = 0; i < ListaDeAreas.Count(); i++)
                    {
                        Areas area = db.Areas.Find(ListaDeAreas[i]);
                        listaAreas.Add(area);
                        auxAreas += area.AreaID + " ";
                    }
                potencialAluno.ListaAreas = listaAreas;

                //**************************************************** Lista de Tipo de Cursos ************
                // será que escolheu algum tipo de curso?
                if (ListaDeTipoCurso != null)
                    for (int i = 0; i < ListaDeTipoCurso.Count(); i++)
                    {
                        TipoCurso tipocurso = db.TipoCurso.Find(ListaDeTipoCurso[i]);
                        listaTipoCurso.Add(tipocurso);
                        auxTipoCursos += tipocurso.TipoID + " ";
                    }
                potencialAluno.ListaTipoCurso = listaTipoCurso;



                if (ModelState.IsValid)
                {
                    // adiciona o objeto 'PotencialAluno' à base de dados
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
                ModelState.AddModelError("", "Foram introduzidos dados incorrectos. Verifica, por favor, a sua correção.");
            }

            // vamos reapresentar a View ao Potencial Aluno   
            // recuperando os dados anteriormente fornecidos
            ViewBag.AuxAreas = auxAreas;
            ViewBag.AuxCursos = auxCursos;
            ViewBag.AuxTiposCurso = auxTipoCursos;

            ViewBag.Areas = db.Areas.OrderBy(a => a.NomeArea).ToList();
            ViewBag.Cursos = db.Cursos.OrderBy(c => c.EscolaFK).OrderBy(c => c.Areas.NomeArea).ToList();
            ViewBag.TipoCurso = db.TipoCurso.OrderBy(t => t.Tipo).ToList();

            return View(potencialAluno);
        }

        // GET: PotencialAluno/ConfirmaAluno/5
        [AllowAnonymous]
        public ActionResult ConfirmaAluno(string id)
        {
            // avalia se o parâmetro é nulo
            // id = código GUID associado ao aluno
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            PotencialAluno potencialAluno = db.PotencialAluno.Where(a => a.CodigoIdentificacao.Equals(id)).FirstOrDefault();

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

                return RedirectToAction("EditarAluno");
            }

            // é necessário fazer aqui o mesmo que se fez no create, para preservar as escolhas do utilizador...

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

              //  potencialAluno.CodigoIdentificacao = Guid.NewGuid().ToString();
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

                //**************************************************** Lista de Tipo de Cursos ************
                if (ListaDeTipoCurso != null)
                    for (int i = 0; i < ListaDeTipoCurso.Count(); i++)
                    {
                        TipoCurso tipocurso = db.TipoCurso.Find(ListaDeTipoCurso[i]);
                        listaTipoCurso.Add(tipocurso);
                    }
                potencialAluno.ListaTipoCurso = listaTipoCurso;


                if (ModelState.IsValid)
                {

                    db.Entry(potencialAluno).State = System.Data.Entity.EntityState.Modified;
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
