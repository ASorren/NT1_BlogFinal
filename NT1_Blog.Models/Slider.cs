using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NT1_Blog.Models
{
    public class Slider
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre para el slider")]
        [Display(Name = "Nombre Slider")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name ="Imagen")]
        public string UrlImagen { get; set; }
        
    }
}
