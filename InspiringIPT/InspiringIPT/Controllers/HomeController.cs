using InspiringIPT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InspiringIPT.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.Cursos.ToList().OrderBy(n => n.NomeCurso).Take(1));
        }
        public ActionResult notFound()
        {
            return RedirectToAction("Index", "Home");
            //return View();
        }
        //// GET: PotencialAluno/Create
        //public ActionResult Inscricao()
        //{
        //    ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea");
        //    ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso");
        //    ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo");
        //    return View();
        //}

        //// POST: PotencialAluno/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Inscricao([Bind(Include = "AlunoID,CursoID,AreaID,TipoID,NomeCompleto,Email,Concelho,DataNascimento,Contacto,Genero,DataInscricao,HabAcademicas,CursosFK,AreasFK,TiposCursosFK")] PotencialAluno potencialAluno)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.PotencialAluno.Add(potencialAluno);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.AreasFK = new SelectList(db.Areas, "AreaID", "NomeArea", potencialAluno.AreasFK);
        //    ViewBag.CursosFK = new SelectList(db.Cursos, "CursoID", "NomeCurso", potencialAluno.CursosFK);
        //    ViewBag.TiposCursosFK = new SelectList(db.TipoCurso, "TipoID", "Tipo", potencialAluno.TiposCursosFK);
        //    return View(potencialAluno);
        //}
        //Home/About
        public ActionResult SobreNos()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        //Home/Contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //Home/IPT
        public ActionResult Equipa()
        {
            ViewBag.Message = "Saiba quem somos.";

            return View();
        }
        public ActionResult Noticia()
        {
            ViewBag.Message = "Saiba quem somos.";

            return View();
        }
        public ActionResult Agenda()
        {
            ViewBag.Message = "Saiba quem somos.";

            return View();
        }
    }
}