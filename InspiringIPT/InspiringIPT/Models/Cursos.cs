using System;
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

        [Required(ErrorMessage = "Introduzir o Nome do Curso")]
        [Display(Name = "Cursos:")]
        public string NomeCurso { get; set; }

        [Required(ErrorMessage = "Introduzir Sigla do Curso")]
        [Display(Name = "Siglas:")]
        public string SiglaCurso { get; set; }

        [Required(ErrorMessage = "Introduzir uma Descrição sobre o Curso")]
        [Display(Name = "Descrição:")]
        public string Descricao { get; set; }

        // ********************************************************************
        // criar a chave forasteira, e cria um atributo 
        // relaciona o objeto Cursos com um objeto Área
        [ForeignKey("Areas")]
        [Display(Name = "Áreas:")]
        [Required(ErrorMessage = "Selecionar uma Área válida")]
        public int AreaFK { get; set; }
        public virtual Areas Areas { get; set; }

        // criar a chave forasteira
        // cria um atributo para funcionar como FK, na BD
        // e relaciona-o com o atributo anterior
        [ForeignKey("TipoCurso")]
        [Display(Name = "Tipos de Cursos:")]
        [Required(ErrorMessage = "Selecionar uma um Tipo de Curso")]
        public int TipoCursoFK { get; set; }
        public virtual TipoCurso TipoCurso { get; set; }

        // criar a chave forasteira
        // cria um atributo para funcionar como FK, na BD
        // e relaciona-o com o atributo anterior
        [ForeignKey("Escola")]
        [Display(Name = "Escolas:")]
        [Required(ErrorMessage = "Selecionar uma Escola válida")]
        public int EscolaFK { get; set; }
        public virtual Escola Escola { get; set; }

        // um CURSO tem uma coleção de POTENCIAIS ALUNOS
        public virtual ICollection<PotencialAluno> ListaPotencialAluno { get; set; }


    }
}

