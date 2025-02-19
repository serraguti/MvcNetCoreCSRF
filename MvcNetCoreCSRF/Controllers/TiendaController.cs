using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreCSRF.Controllers
{
    public class TiendaController : Controller
    {
        public IActionResult Productos()
        {
            //SI EL USUARIO NO SE HA VALIDADO TODAVIA
            //LO LLEVAMOS A DENIED
            if (HttpContext.Session.GetString("USUARIO") == null)
            {
                return RedirectToAction("Denied", "Managed");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Productos(string direccion
            , string[] producto)
        {
            if (HttpContext.Session.GetString("USUARIO") == null)
            {
                return RedirectToAction("Denied", "Managed");
            }
            else
            {
                //MEDIANTE TEMPDATA SE ALMACENA LA INFORMACION
                //PARA SER ENVIADA A OTROS CONTROLLERS O 
                //METODOS IACTIONRESULT EN LAS REDIRECCIONES
                TempData["PRODUCTOS"] = producto;
                TempData["DIRECCION"] = direccion;
                return RedirectToAction("PedidoFinal");
            }
        }

        public IActionResult PedidoFinal()
        {
            //AQUI NECESITO LOS PRODUCTOS Y LA DIRECCION
            //DEL METODO POST DE PRODUCTOS.  COMO LOS RECUPERO AQUI??
            string[] productos = TempData["PRODUCTOS"]
                as string[];
            ViewData["DIRECCION"] = TempData["DIRECCION"];
            return View(productos);
        }
    }
}
