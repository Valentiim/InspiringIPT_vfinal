using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace InspiringIPT.Models
{
    public class OutrosCursos
    {
        /* ***********************************************************************
       Nesta classe é que está o 'segredo' para representar o relacionamento
       entre a classe C e a classe D num relacionamento Muitos-para-Muitos,
       com atributos do relacionamento.
       
       Nesta classe vão ser representados dois relacionamentos 1-Muitos,
       com as respetivas chaves forasteiras (FK).
          - Uma, para referenciar a classe C
          - e outra, para referenciar a classe D
      
       Ao contrário do que é caraterístico de um relacionamento M-N, em SQL,
       aqui existirá uma chave primária com um único atributo.
       A chave primária com os dois atributos (que são, também, chave forasteira)
       não é aqui permitida.

      
       Os atributos destas classes são atributos genéricos e servem apenas
       para ilustrar este processo.
      *********************************************************************** */
        [Key]
        public int OutrosID { get; set; }

        public string OutrasFormacoes { get; set; }

        //***********************************************************************
        // definição da chave forasteira (FK) que referencia a classe Tipo do Curso
        //***********************************************************************
        [ForeignKey("TipoCursoFK")]
        public int TipoCursoFK { get; set; }
        public TipoCurso TipoCurso { get; set; }

        //***********************************************************************
        // definição da chave forasteira (FK) que referencia a classe Tipo do Curso
        //***********************************************************************
        [ForeignKey("PotencialAlunoFK")]
        public int PotencialAlunoFK { get; set; }
        public virtual PotencialAluno PotencialAluno { get; set; }
        //***********************************************************************
    }
}