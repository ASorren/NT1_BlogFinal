using System;
using System.Collections.Generic;
using System.Text;

namespace NT1_Blog.Models.ViewModels
{
   public class HomeVM
    {
        // va a mostrar los enum de slider y articulos
       public IEnumerable<Slider> Slider { get; set; }

        public IEnumerable<Articulo> ListaArticulos { get; set; }

    }
}
