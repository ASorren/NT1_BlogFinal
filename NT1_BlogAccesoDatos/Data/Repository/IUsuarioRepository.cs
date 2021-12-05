using NT1_Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NT1_BlogAccesoDatos.Data.Repository
{
   public interface IUsuarioRepository : IRepository<ApplicationUser>
    {

        void BloqueaUsuario(string IdUsuario);

        void DesbloqueaUsuario(string IdUsuario);

    }
}
