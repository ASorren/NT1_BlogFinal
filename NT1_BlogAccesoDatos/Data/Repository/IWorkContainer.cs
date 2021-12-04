using System;
using System.Collections.Generic;
using System.Text;

namespace NT1_BlogAccesoDatos.Data.Repository
{
   public interface IWorkContainer : IDisposable
    {

        ICategoriaRepository Categoria { get; }
        IArticuloRepository Articulo { get; }
        ISliderRepository Slider { get; }
        //Further Repositories to be added like Categoria above

        void Save();
    }
}
