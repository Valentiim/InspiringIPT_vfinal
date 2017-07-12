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

        [Display(Name = "Tipos de Cursos")]
        public string Tipo { get; set; }

        //***************************************************************************
        //TIPO DE CURSO tem uma coleção de CURSOS
        public virtual ICollection<Cursos> ListaCursos { get; set; }

        //TIPO DE CURSO tem uma coleção de POTENCIAIS ALUNOS
        public virtual ICollection<PotencialAluno> ListaPotencialAluno { get; set; }


    }
}