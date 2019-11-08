using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoriasUTEapp.Areas.Api.Models;

namespace TutoriasUTEapp.Areas.Api.Controllers
{
    public class AndroidLoginController : Controller
    {
        public JsonResult Login(string us, string pass, int role)
        {
            //se crea el objeto login
            AndroidLogin login = new AndroidLogin();
            login.user = us;
            login.pass = pass;
            login.role = role;

            return Json(AndroidLoginManager.isLoged(login), JsonRequestBehavior.AllowGet);
        }
    }
}
