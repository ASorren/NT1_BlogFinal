using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NT1_Blog.Models.ViewModels;
using NT1_BlogAccesoDatos.Data.Repository;

namespace NT1_Blog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IWorkContainer _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticulosController(IWorkContainer contenedorTrabajo, IWebHostEnvironment hostingEnvirnoment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvirnoment;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();

        }

        //correccion error, para el Create necesitamos el viewModel de articulo, para sacar los nombres en la lista.

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM artivm = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetCategoryList()
            };

            return View(artivm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                if (artiVM.Articulo.Id == 0)
                {
                    // nuevo articulo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();
                    _contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
            }

            // esto corrige el error que rompe cuando se envia vacio, asi lo hace obligatorio, se manda la lista de categorias.
            artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetCategoryList();
            return View(artiVM);
        }

        // devuelve la vista
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticuloVM artivm = new ArticuloVM()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetCategoryList()
            };

            if (id != null)
            {
                artivm.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            }

            return View(artivm);
        }


        // se encarga de hacer el update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM artiVM)

        {

            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                // obtenemos el articulo a validar por db
                var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(artiVM.Articulo.Id);

                if (archivos.Count() > 0)
                {
                    // se edita imagen
                    // toda esta merda se encarga de manejar el path de la foto, si la encuentra arma el path y cambia el que ya está por el update
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    // obtenemos ruta de imagen
                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    // subimos de nuevo el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + nuevaExtension;
                    artiVM.Articulo.FechaCreacion = DateTime.Now.ToString();
                    _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // si la imagen ya existe y no se reemplaza, debe conservar la ruta actual
                    artiVM.Articulo.UrlImagen = articuloDesdeDb.UrlImagen;
                }
                _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(id);
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));
           
            
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if(articuloDesdeDb == null)
            {
                return Json(new {success = false, message = "Error borrando articulo"});
            }

            _contenedorTrabajo.Articulo.Remove(articuloDesdeDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Articulo borrado correctamente" });
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
