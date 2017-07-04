using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InspiringIPT.Models
{
    public class ListaColaboradores
    {
        public int ColaboradorID { get; set; }

        [Display(Name = "Colaborador:")]
        public string Nome { get; set; }

        public string Apelido { get; set; }

        [Display(Name = "Localidade")]
        public string Localidade { get; set; }

        [Display(Name = "Contacto")]
        public string Contacto { get; set; }

        [Display(Name = "NIF")]
        public string NIF { get; set; }

        [Display(Name = "E-Mail")]
        public string EMAIL { get; set; }
    }
}