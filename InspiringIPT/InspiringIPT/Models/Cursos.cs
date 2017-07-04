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
            ListaAreaAluno = new HashSet<PotencialAluno>();
      

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
        [Display(Name = "Àrea de Interesse:")]
        public int AreaFK { get; set; }
        public virtual Areas Areas { get; set; }
        [Required(ErrorMessage = "Selecionar uma um Tipo de Curso")]
        [Display(Name = "Tipo de Curso:")]
        public int TipoCursoFK { get; set; }
        public TipoCurso TipoCurso { get; set; }
        [Required(ErrorMessage = "Selecionar uma Escola válida")]
        [Display(Name = "Escola:")]
        public int EscolaFK { get; set; }
        public Escola Escola { get; set; }
   
        public virtual ICollection<PotencialAluno> ListaAreaAluno { get; set; }
       

    }
}

