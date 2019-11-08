using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TutoriasUTEapp.Controllers
{
    public class MainPageController : Controller
    {
        public ActionResult Inicio()
        {
            if (Session["Admin"] != null)
            {
                return View();
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult Salir()
        {
            //se borra la sesión
            Session["Admin"] = null;
            return RedirectToAction("Login", "Login");
        }
    }
}
