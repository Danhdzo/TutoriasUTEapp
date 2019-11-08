using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoriasUTEapp.Models;
using TutoriasUTEapp.ViewModels;

namespace TutoriasUTEapp.Controllers
{
    public class LoginController : Controller
    {

        public static TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel objLogin)
        {
            if (ModelState.IsValid)
            {
                //si fueramos a usar la encriptacion
                //string encryptedPass = EncryptionDecryption.EncriptarSHA1(objLogin.Password);

                //guardamos lo que nos pasó el usuario en variables
                string UserName = objLogin.UserName.ToString();
                string UserPassword = objLogin.UserPassword.ToString();

                //obtenemos el id del tutor con ese usiario y contraseña
                var isLogged = (from a in dbCtx.Administrators
                                where a.UserName.Equals(UserName) && a.UserPassword.Equals(UserPassword)
                                select new
                                {
                                    AdminUser = a.UserName
                                }).SingleOrDefault();

                //si si se encontró un tutor con los parametros obtenidos entonces
                if (isLogged != null)
                {
                    #region LOG
                    //se guarda el inicio de sesión en la tabla log de la base de datos

                    //se crea el objeto log
                    Log log = new Log();

                    //se le asignan los valores
                    log.Date = DateTime.Now;
                    log.Description = "Se accedió como administrador con el usuario: " + isLogged.AdminUser.ToString();

                    //se guarda en la base de datos
                    dbCtx.Logs.Add(log);
                    dbCtx.SaveChanges();

                    #endregion 

                    //se guarda el usuario que entró en una variable de sesión
                    Session["Admin"] = isLogged.AdminUser.ToString();

                    //manda al inicio
                    return RedirectToAction("Inicio", "MainPage");
                }
            }
            //si no se inicio sesion correctamente entonces segirá en la pagina de login 
            return View(objLogin);
        }
    }
}
