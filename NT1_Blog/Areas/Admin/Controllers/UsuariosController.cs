using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NT1_BlogAccesoDatos.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NT1_Blog.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UsuariosController : Controller
    {

        private readonly IWorkContainer _contenedorTrabajo;

        public UsuariosController(IWorkContainer contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return View(_contenedorTrabajo.Usuario.GetAll(u => u.Id != usuarioActual.Value));
        }

        public IActionResult Bloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.BloqueaUsuario(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Desbloquear(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _contenedorTrabajo.Usuario.DesbloqueaUsuario(id);
            return RedirectToAction(nameof(Index));
        }

    }

}
