using NT1_Blog.AccesoDatos.Data;
using NT1_Blog.Models;
using NT1_BlogAccesoDatos.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NT1_BlogAccesoDatos.Data
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;


        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void BloqueaUsuario(string IdUsuario)
        {
            var usuario = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuario.LockoutEnd = DateTime.Now.AddHours(1);
            _db.SaveChanges();
        }

        public void DesbloqueaUsuario(string IdUsuario)
        {
            var usuario = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuario.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
