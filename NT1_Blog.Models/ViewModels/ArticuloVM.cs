using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace NT1_Blog.Models.ViewModels
{
    // esto sirve para al instanciar el ArticuloVM ya podemos acceder a todos los articulos dentro de su lista
    public class ArticuloVM
    {
        public Articulo Articulo { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
