using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NT1_Blog.Models;
//agrega Diego
using NT1_BlogAccesoDatos.Data.Repository;
using NT1_Blog.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace NT1_Blog.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IWorkContainer _contenedorTrabajo;

        public HomeController(IWorkContainer contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            //se llama al homeVM, para poder mostrar lo que contiene
            HomeVM homeVm = new HomeVM()
            {
                //llamamos al slider y al articulo
                Slider = _contenedorTrabajo.Slider.GetAll(),
                ListaArticulos = _contenedorTrabajo.Articulo.GetAll()
            };
            return View(homeVm);
        }

        //recibe el id del articulo dese el index.cshtml de Cliente/View/Home. Para
        //pasarcelo tenemos que hacer una lambda(?) que devuelve el Articulo de la BD cuyo Id es 
        // igual al id pasado por parametro
        [Authorize]
        public IActionResult Details(int id)
        {
            var articulosDesdeDb = _contenedorTrabajo.Articulo.GetFirstOrDefault(art => art.Id == id);
            return View(articulosDesdeDb);
        }
        
    }
}
