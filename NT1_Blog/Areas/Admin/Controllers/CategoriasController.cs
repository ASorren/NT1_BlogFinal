using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NT1_Blog.Models;
using NT1_BlogAccesoDatos.Data.Repository;

namespace NT1_Blog.Areas.Admin
{
    [Area("Admin")] // Solo accede el admin 
    public class CategoriasController : Controller
    {
        private readonly IWorkContainer _contenedorTrabajo;

        public CategoriasController(IWorkContainer contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }     
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #region Llamadas a la Api
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }


        #endregion
    }
}
