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
            ListaCursos = new HashSet<Cursos>();
            ListaPotencialAluno = new HashSet<PotencialAluno>();
        }


        [Key]
        public int TipoID { get; set; }
        [Display(Name = "Tipo de curso: ")]
        public string Tipo { get; set; }

        //***************************************************************************
        // lista de cursos associados ao tipo
        public virtual ICollection<Cursos> ListaCursos { get; set; }

        // especificar que um Tipo de Curso tem um ou muitos Alunos
        public virtual ICollection<PotencialAluno> ListaPotencialAluno { get; set; }


    }
}