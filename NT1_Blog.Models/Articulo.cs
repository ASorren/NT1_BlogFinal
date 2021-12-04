using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace NT1_Blog.Models
{
    public class Articulo
    {
        [Key]
        public int id { get; set; }
        public string Nombre { get; set; }
        public string FechaCreacion { get; set; }
        public string UrlImagen { get; set; }
        public string Descripcion { get; set; }
    }
}
