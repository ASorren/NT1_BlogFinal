using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NT1_Blog.Models.ViewModels;
using NT1_BlogAccesoDatos.Data.Repository;

namespace NT1_Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IWorkContainer _contenedorTrabajo;

        public ArticulosController(IWorkContainer contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            ArticuloVM artivm = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetCategoryList()
            };

           return View(artivm);
          
        }

        #region LLAMADAS A LA API
        [HttpGet]

        public IActionResult GetAll()

        {
                // el includeProperties es para acceder a la categoria ya que articulo se encuentra dentro
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }


        /*
        [HttpDelete]
        
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Articulo.Get(id);
            if(objFromDb == null)
            {
                return Json(new { succes = false, msessage = "Error borrando articulo" });
            }

            _contenedorTrabajo.Articulo.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { succes = true, message = "Articulo borrado correctamente" });
        }
        */
        #endregion

    }
}
