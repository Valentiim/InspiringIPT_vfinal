using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InspiringIPT.Models
{
    public partial class PotencialAluno
    {
        public PotencialAluno()
        {

            ListaCursos = new HashSet<Cursos>();
            ListaTipoCurso = new HashSet<TipoCurso>();
            ListaAreas = new HashSet<Areas>();
            ListaOutrasAreas = new HashSet<OutrasAreas>();
            ListaOutrosCursos = new HashSet<OutrosCursos>();

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // impede o atributo de ser AutoNumber
        public int AlunoID { get; set; } 
        public int CursoID { get; set; }
        public int AreaID { get; set; }
        public int TipoID { get; set; }

        [Required(ErrorMessage = "Deve introduzir o {0}...")]
       // [RegularExpression("[A-Z][a-záéíóúàèìòùâêîôûãõäëïöüñç]+(( |'|-|( (de|das|dos|e) )|( d'))[A-Z][a-záéíóúàèìòùâêîôûãõäëïöüñç]+)*", ErrorMessage = "No '{0}' só pode usar letras. Cada palavra começa com uma letra maiúscula.")]
        [Display(Name = "Nome do Aluno:")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Introduzir um e-mail válido")]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        [Display(Name = "Concelho: ")]
        public string Concelho { get; set; }

        [Required]
        [Display(Name = "D.Nascimento:")]
        [Column(TypeName = "date")] // formata o tipo de dados na BD
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascimento { get; set; } // o '?' torna o preenchimento do atributo facultativo

        [Required]
        [Display(Name = "Contacto:")]
        [StringLength(9)]
        //[RegularExpression("[0-9]{9}", ErrorMessage = "O Contacto é composto por 9 caracteres Numéricos")]
        public string Contacto { get; set; }

        [Required]
        [StringLength(1)] //permite apenas um algarismo
        [Display(Name = "Sexo:")]
        public string Genero { get; set; }

        [Column(TypeName = "date")]// formata o tipo de dados na BD
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataInscricao { get; set; }  // o '?' torna o preenchimento do atributo facultativo

        [Required]
        [Display(Name = "Habilitação:")]
        public string HabAcademicas { get; set; }

        // ********************************************************************++
        // criar a chave forasteira, e cria um atributo que 
        // relaciona o objeto "PotencialAluno" com um objeto "Curso"
        public int CursosFK { get; set; }
        public Cursos Curso { get; set; }

        // ********************************************************************++
        // criar a chave forasteira, e cria um atributo que 
        // relaciona o objeto "PotencialAluno" com um objeto "Áreas"
        public int AreasFK { get; set; }
        public Areas Area { get; set; }

        // ********************************************************************++
        // criar a chave forasteira, e cria um atributo que 
        // relaciona o objeto "PotencialAluno" com um objeto "Tipo de Curso"
        public TipoCurso TipoC { get; set; }
        public int TiposCursosFK { get; set; }
        public string UserID { get; set; }

        public virtual ICollection<Cursos> ListaCursos { get; set; }//associados o objeto potencial aluno existe um objeto Curso
        public virtual ICollection<OutrosCursos> ListaOutrosCursos { get; set; }//associados o objeto potencial aluno existe um objeto outros cursos
        public virtual ICollection<TipoCurso> ListaTipoCurso { get; set; } //associados o objeto potencial aluno existe um objeto outras areas
        public virtual ICollection<Areas> ListaAreas { get; set; } //associados o objeto potencial aluno existe um objeto outros cursos
        public virtual ICollection<OutrasAreas> ListaOutrasAreas { get; set; }//associados o objeto potencial aluno existe um objeto outras areas

    }

}