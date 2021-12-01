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
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {

        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public IEnumerable<SelectListItem> GetCategoryList()
        {
            return _db.Categoria.Select(element => new SelectListItem()
            {
                Text = element.Nombre,
                Value = element.Id.ToString()
            }) ;
        }

        public void Update(Categoria category)
        {
            var dbObj = _db.Categoria.FirstOrDefault(s => s.Id == category.Id);

            dbObj.Nombre = category.Nombre;
            dbObj.Orden = category.Orden;

            _db.SaveChanges();
        }
    }
}
