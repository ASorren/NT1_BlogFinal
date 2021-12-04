using Microsoft.AspNetCore.Mvc.Rendering;
using NT1_Blog.AccesoDatos.Data;
using NT1_Blog.Models;
using NT1_BlogAccesoDatos.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NT1_BlogAccesoDatos.Data
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {

        private readonly ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(Articulo articulo)
        {
            var dbObj = _db.Articulo.FirstOrDefault(s => s.Id == articulo.Id);

            dbObj.Nombre = articulo.Nombre;
            dbObj.Descripcion = articulo.Descripcion;
            dbObj.UrlImagen = articulo.UrlImagen;
            dbObj.CategoriaId = articulo.CategoriaId;

            //_db.SaveChanges();
        }
    }
}
