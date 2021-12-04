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
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {

        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(Slider slider)
        {
            var dbObj = _db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            dbObj.Nombre = slider.Nombre;
            dbObj.Estado = slider.Estado;
            dbObj.UrlImagen = slider.UrlImagen;

            _db.SaveChanges();
        }
    }
}
