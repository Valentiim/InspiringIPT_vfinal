using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InspiringIPT.Models
{
    public class TipoCurso
    {
        public TipoCurso()
        {
            // vai representar os dados da tabela do Tipo de Curso

            // criar o construtor desta classe
            // e carregar a lista dos Cursos
            ListaPotencialAluno = new HashSet<PotencialAluno>();

            ListaOutrosCursos = new HashSet<OutrosCursos>();

        }
        [Key]
        public int TipoID { get; set; }
        [Display(Name = "Tipo: ")]
        public string Tipo { get; set; }

        // especificar que um Tipo de Curso tem nenhum ou muitos Cursos
        public virtual ICollection<PotencialAluno> ListaPotencialAluno { get; set; }

        public virtual ICollection<OutrosCursos> ListaOutrosCursos { get; set; }

    }
}