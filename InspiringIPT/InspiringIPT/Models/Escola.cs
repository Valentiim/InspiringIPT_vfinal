using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InspiringIPT.Models
{
    public partial class Escola
    {
        public Escola()
        {
            // vai representar os dados da tabela da Escola

            // criar o construtor desta classe
            // e carregar a lista dos Cursos
            ListaCursos = new HashSet<Cursos>();
        }
        [Key]
        public int EscolaID { get; set; }
        [Display(Name = "Escola:")]
        public string NomeEscola { get; set; }
        [Display(Name = "Sigla:")]
        public string SiglaEscola { get; set; }

        // especificar que uma Escola tem um ou muitos Cursos
        public ICollection<Cursos> ListaCursos { get; set; }

    }
}