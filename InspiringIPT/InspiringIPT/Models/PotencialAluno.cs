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

        }
        [Key]
        public int AlunoID { get; set; }
        public int CursoID { get; set; }
        public int AreaID { get; set; }
        public int TipoID { get; set; }

        [Required(ErrorMessage = "Introduzir o seu nome completo")]
        //[RegularExpression("[A-ZÉÓÁÍÂ][a-záéíóúàèìòù]+(( [ed][aeo]?(s)?)?[ -'][A-ZÉÓÁÍÂ][a-záéíóúàèìòù]+){1, 4}")]
        [Display(Name = "Nome:")]
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
        [RegularExpression("[0-9]{9}", ErrorMessage = "O Contacto é composto por 9 caracteres Numéricos")]
        public string Contacto { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Sexo:")]
        public string Genero { get; set; }

        [Column(TypeName = "date")]// formata o tipo de dados na BD
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataInscricao { get; set; }  // o '?' torna o preenchimento do atributo facultativo

        [Required]
        [Display(Name = "Habilitação:")]
        public string HabAcademicas { get; set; }

        public Cursos Curso { get; set; }
        [ForeignKey("Curso")]
        [Display(Name = "Cursos:")]
        public int CursosFK { get; set; }

        public Areas Area { get; set; }
        [ForeignKey("Area")]
        [Display(Name = "Área:")]
        public int AreasFK { get; set; }

        public TipoCurso TipoC { get; set; }
        [ForeignKey("TipoC")]
        [Display(Name = "Tipo:")]
        public int TiposCursosFK { get; set; }
        public string UserID { get; set; }

        //herança
        public virtual ICollection<Cursos> ListaCursos { get; set; }//associados o objeto potencial aluno existe um objeto Curso
        public virtual ICollection<TipoCurso> ListaTipoCurso { get; set; } //associados o objeto potencial aluno existe um objeto outras areas
        public virtual ICollection<Areas> ListaAreas { get; set; } //associados o objeto potencial aluno existe um objeto outros cursos

    }

}