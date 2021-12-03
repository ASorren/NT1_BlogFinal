using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        
        public IActionResult Index()
        {
            return View();
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
