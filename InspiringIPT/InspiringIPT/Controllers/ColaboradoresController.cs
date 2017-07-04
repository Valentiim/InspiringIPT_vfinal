using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using InspiringIPT.Models;
using Microsoft.AspNet.Identity;

namespace InspiringIPT.Controllers
{
    public class ColaboradoresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Colaboradores
        [HttpGet]
        [Authorize]
        public ActionResult Perfil()
        {
            var userid = User.Identity.GetUserId();
            var user = (from c in db.Colaboradores where c.UserID == userid select c).Single();
            ViewBag.colaborador = user;
            return View(user);
        }
       // [Authorize(Roles = "Gestores")]
        [AllowAnonymous]
        public ActionResult Listagem()
        {
            IEnumerable<ListaColaboradores> colaborador =
                from c in db.Colaboradores
                join u in db.Users on c.UserID equals u.Id
                select new ListaColaboradores
                {
                    ColaboradorID = c.ColaboradorID,
                    Nome = c.NomeProprio,
                    Apelido = c.Apelido,
                    Localidade = c.Localidade,
                    Contacto = c.Contacto,
                    NIF = c.NIF,
                    EMAIL = u.Email,
                };
            return View(colaborador.ToList());
        }


        // GET: Colaboradores
        public ActionResult Index()
        {
            return RedirectToAction("Perfil");
        }

        // GET: Colaboradores/Details/5
        // [Authorize(Roles = "Gestores")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Colaboradores colaboradores = db.Colaboradores.Find(id);
            if (colaboradores == null)
            {
                return RedirectToAction("Index");
            }
            return View(colaboradores);
        }

        // GET: Colaboradores/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "Gestores")]
       // [ValidateAntiForgeryToken]
        public ActionResult Create(Colaboradores model)
        {
            var user = User.Identity.GetUserId();
            var newId = db.Colaboradores.Select(x => x.ColaboradorID).Count() + 1;
            var colaborador = new Colaboradores { UserID = user, ColaboradorID= newId, NomeProprio=model.NomeProprio, Apelido=model.Apelido, Contacto=model.Contacto, Localidade=model.Localidade, NIF=model.NIF };
            //Guarda os dados do colaborador na Base de Dados
            db.Colaboradores.Add(colaborador);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // GET: Colaboradores/Edit/5
        //[Authorize(Roles = "Gestores")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            Colaboradores colaboradores = db.Colaboradores.Find(id);
            if (colaboradores == null)
            {
                return RedirectToAction("Index");
            }
            return View(colaboradores);
        }

        // POST: Colaboradores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //  [Authorize(Roles = "Colaboradores")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ColaboradorID,NIF,NomeProprio,Apelido,Localidade,Contacto,UserID")] Colaboradores colaboradores)
        {
           
            if (ModelState.IsValid)
            {
               
                db.Entry(colaboradores).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = colaboradores.ColaboradorID });
            }
            
            return View(colaboradores);
        }

        // GET: Colaboradores/Edit/5
        [Authorize]
        public ActionResult Editar()
        {
            var userid = User.Identity.GetUserId();
            var user = (from c in db.Colaboradores where c.UserID == userid select c.ColaboradorID).Single();
            ViewBag.colaborador = user;
            Colaboradores colaboradores = db.Colaboradores.Find(user);
            if (colaboradores == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(colaboradores);
        }

        // POST: Colaboradores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      // [Authorize(Roles = "Gestores")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "ColaboradorID,NIF,NomeProprio,Apelido,Localidade,Contacto,UserID")] Colaboradores colaboradores)
        {
            
            colaboradores.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                
                db.Entry(colaboradores).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Perfil");
            }
            
            return View(colaboradores);
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
