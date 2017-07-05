using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InspiringIPT.Models
{
    public partial class Areas
    {
        public Areas()
        {
            // vai representar os dados da tabela dos Áreas

            // criar o construtor desta classe
            // e carregar a lista dos Cursos
        ListaPotencialAluno = new HashSet<PotencialAluno>();
            
        }
        [Key]
        public int AreaID { get; set; }
        [Display(Name = "Àrea:")]
        public string NomeArea { get; set; }

        // especificar que uma Área tem um ou muitos Cursos

        public virtual ICollection<PotencialAluno> ListaPotencialAluno { get; set; }
    }
}