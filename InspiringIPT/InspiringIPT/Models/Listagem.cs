using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InspiringIPT.Models
{
    public class Listagem
    {

        [Display(Name = "Colaborador:")]
        public string Nome { get; set; }
        public string Apelido { get; set; }
    }   
}