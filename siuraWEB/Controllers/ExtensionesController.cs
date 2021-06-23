using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace siuraWEB.Controllers
{
    public class ExtensionesController : Controller
    {
        // FUNCION QUE DEVUELVE  LA VISTA MODAL DE EXTENSIONES
        public ActionResult Index()
        {
            return View();
        }
    }
}