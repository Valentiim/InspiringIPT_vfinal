using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InspiringIPT.Models
{
    public class Imagens
    {
        [Key]
        public int ImagensID { get; set; }

        public string Categoria { get; set; }

        public DateTime Data { get; set; }

        public  string Nome { get; set; }

    }
}