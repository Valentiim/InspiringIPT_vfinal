﻿using System;
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

        [Required(ErrorMessage = "Deve introduzir o {0}...")]
        // [RegularExpression("[A-Z][a-záéíóúàèìòùâêîôûãõäëïöüñç]+(( |'|-|( (de|das|dos|e) )|( d'))[A-Z][a-záéíóúàèìòùâêîôûãõäëïöüñç]+)*", ErrorMessage = "No '{0}' só pode usar letras. Cada palavra começa com uma letra maiúscula.")]
        [Display(Name = "Nome do Aluno:")]
        public string NomeCompleto { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Introduzir um e-mail válido")]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        [Display(Name = "Concelho: ")]
        public string Concelho { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Nascimento:")]
        [Column(TypeName = "date")] // formata o tipo de dados na BD
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Contacto telefónico:")]
        [StringLength(9, ErrorMessage = "O {0} tem de ser escrito com {1}.", MinimumLength = 9)]
        [RegularExpression("[0-9]{9}")]
        public string Contacto { get; set; }

        [StringLength(1)] //permite apenas um algarismo
        [Display(Name = "Sexo:")]
        [RegularExpression("[MF]", ErrorMessage = "No '{0}' só pode usar a letra M ou F.")]
        public string Genero { get; set; }

        [Required]
        [Display(Name = "Habilitações:")]
        public string HabAcademicas { get; set; }

        //*********************************************************************************************

        [Column(TypeName = "date")]// formata o tipo de dados na BD
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Inscrição:")]
        public DateTime DataInscricao { get; set; }  // torna o preenchimento do atributo facultativo

        [Display(Name = "Código Identificação:")]
        public string CodigoIdentificacao { get; set; } // permite aceder aos dados do potencial aluno, se ele souber este código de identificação

        //*********************************************************************************************

        [Display(Name = "Curso(s):")]
        //associados o objeto potencial aluno existe uma  lista de objeto Curso
        public virtual ICollection<Cursos> ListaCursos { get; set; }

        [Display(Name = "Área(s):")]
        //associados o objeto potencial aluno existe um objeto outros cursos
        public virtual ICollection<Areas> ListaAreas { get; set; }

        [Display(Name = "Tipo(s) de Curso(s):")]
        //associados o objeto potencial aluno existe um objeto outras areas
        public virtual ICollection<TipoCurso> ListaTipoCurso { get; set; }
    }

}