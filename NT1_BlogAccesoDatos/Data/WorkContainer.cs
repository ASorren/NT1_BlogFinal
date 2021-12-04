using NT1_Blog.AccesoDatos.Data;
using NT1_BlogAccesoDatos.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NT1_BlogAccesoDatos.Data
{
    public class WorkContainer : IWorkContainer
    {

        private readonly ApplicationDbContext _db;

        public WorkContainer(ApplicationDbContext db)
        {
            _db = db;
            Categoria = new CategoriaRepository(_db);
            Articulo = new ArticuloRepository(_db);
        }
        public ICategoriaRepository Categoria { get; private set; }
        public IArticuloRepository Articulo { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        } 

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
