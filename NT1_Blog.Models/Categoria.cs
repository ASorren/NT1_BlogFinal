using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NT1_Blog.Models
{
   public class Categoria
    {
        [Key]   
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Ingrese un nombre valido")]
        [Display(Name ="Nombre de la Categoria")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Orden de visualizacion")]
        public string Orden { get; set; }


    }
}
