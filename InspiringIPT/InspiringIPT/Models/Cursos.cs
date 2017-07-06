﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InspiringIPT.Models
{
    public partial class Cursos
    {
        public Cursos()
        {
            // inicialização da lista de um Curso
            ListaPotencialAluno = new HashSet<PotencialAluno>();
          
        }
        [Key]
        public int CursoID { get; set; }

        [Required(ErrorMessage = "Introduzir Nome do Curso")]
        [Display(Name = "Curso:")]
        public string NomeCurso { get; set; }

        [Required(ErrorMessage = "Introduzir Sigla do Curso")]
        [Display(Name = "Sigla do Curso:")]
        public string SiglaCurso { get; set; }

        [Required(ErrorMessage = "Introduzir uma Descrição sobre o Curso")]
        [Display(Name = "Descrição:")]
        public string Descricao { get; set; }

        // **************************
        // criar a chave forasteira, e cria um atributo
        // relaciona o objeto Cursos com um objeto Área
        [Required(ErrorMessage = "Selecionar uma Área válida")]
        //[ForeignKey("Areas")]
        public int AreaFK { get; set; }
        public  Areas Areas { get; set; }


        [Required(ErrorMessage = "Selecionar uma um Tipo de Curso")]
       // [ForeignKey("TipoCurso")]
        public int TipoCursoFK { get; set; }
        public TipoCurso TipoCurso { get; set; }

        // cria um atributo para funcionar como FK, na BD
        // e relaciona-o com o atributo anterior
        //[ForeignKey("Escola")]
        [Required(ErrorMessage = "Selecionar uma Escola válida")]
        public int EscolaFK { get; set; }
        // **************************
        // criar a chave forasteira
        // relaciona o objeto ANIMAL com um objeto DONO
        public Escola Escola { get; set; }
        // um CURSO tem uma coleção de POTENCIAIS ALUNOS
        public virtual ICollection<PotencialAluno> ListaPotencialAluno { get; set; }
       

    }
}

