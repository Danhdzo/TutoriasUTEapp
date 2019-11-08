using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoriasUTEapp.Models;
using TutoriasUTEapp.ViewModels;

namespace TutoriasUTEapp.Controllers
{
    public class RecordatoriosController : Controller
    {
        TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

        [HttpGet]
        public ActionResult Recordatorios()
        {
            if (Session["Admin"] != null)
            {
                //se crea el viewModel comun
                RecordatoriosCommonViewModel model = new RecordatoriosCommonViewModel();
                model.RecordatoriosCreateVM = new RecordatoriosCreateViewModel();
                model.RecordatoriosResultVM = new RecordatoriosResultViewModel();

                //se guarda la lista
                model.RecordatoriosResultVM.Recordatorios = this.CargarRecordatorios();

                return View(model);
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult Recordatorios(RecordatoriosCommonViewModel model)
        {
            if (Session["Admin"] != null)
            {
                if (ModelState.IsValid)
                {
                    //se guarda
                    this.GuardarRecordatorio(model.RecordatoriosCreateVM);

                    return RedirectToAction("Recordatorios", "Recordatorios");
                }
                else
                {
                    //se cargan de nuevo
                    model.RecordatoriosResultVM = new RecordatoriosResultViewModel();
                    model.RecordatoriosResultVM.Recordatorios = this.CargarRecordatorios();

                    return View(model);
                }
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }


        private List<RecordatoriosViewModel> CargarRecordatorios()
        {
            //se crea la lista
            List<RecordatoriosViewModel> recordatorios = new List<RecordatoriosViewModel>();

            //se buscan los recordatorios
            var query = (from r in dbCtx.Reminders
                         join rl in dbCtx.Roles on r.RoleID equals rl.ID
                         orderby r.Date ascending
                         select new
                         {
                             ID = r.ID,
                             Fecha = r.Date,
                             Asunto = r.Subject,
                             Descripcion = r.Description,
                             Rol = rl.Description
                         }).ToList();

            //se crean los objetos
            foreach (var r in query)
            {
                //se crea un recordatorio
                RecordatoriosViewModel recordatorio = new RecordatoriosViewModel();

                //se le asignan los valores actuales
                recordatorio.RecordatorioID = r.ID;
                recordatorio.Fecha = r.Fecha;
                recordatorio.Asunto = r.Asunto;
                recordatorio.Recordatorio = r.Descripcion;
                recordatorio.Rol = r.Rol;


                //se agrega a la lista
                recordatorios.Add(recordatorio);

            }//fin del foreach



            return recordatorios;
        }

        private void GuardarRecordatorio(RecordatoriosCreateViewModel nuevoRecordatorio)
        {
            //se guardan los valores del nuevo maestroVM en variables
            DateTime Fecha = nuevoRecordatorio.Fecha;
            string Asunto = nuevoRecordatorio.Asunto;
            string Description = nuevoRecordatorio.Recordatorio;
            string Rol = nuevoRecordatorio.Rol;
            string RolDesc = null;

            //se crea un nuevo maestro
            Reminder recordatorio = new Reminder();

            //se le asignan los valores
            recordatorio.Date = Fecha;
            recordatorio.Subject = Asunto;
            recordatorio.Description = Description;


            //para agregar el ID role
            if (Rol == "t")
            {
                RolDesc = "Tutores";
                recordatorio.RoleID = 2;
            }
            if (Rol == "d")
            {
                RolDesc = "Docentes";
                recordatorio.RoleID = 1;
            }

            //se agrega a la base de datos
            dbCtx.Reminders.Add(recordatorio);
            dbCtx.SaveChanges();


            //se guarda en la base de datos el log
            #region LOG
            //se guarda el inicio de sesión en la tabla log de la base de datos

            //se crea el objeto log
            Log log = new Log();

            //se le asignan los valores
            log.Date = DateTime.Now;
            log.Description = "Se agregó un recordatorio para: " + RolDesc + ", con asunto: " + Asunto + " con fecha: " + Fecha;

            //se guarda en la base de datos
            dbCtx.Logs.Add(log);
            dbCtx.SaveChanges();

            #endregion
        }


        public ActionResult EliminarRecordatorio(int RecordatorioID)
        {

            if (Session["Admin"] != null)
            {

                //se busca los registros en la tabla de roles que tengan el ID del maestro
                var recordatorio = dbCtx.Reminders.Where(x => x.ID == RecordatorioID).SingleOrDefault();
                dbCtx.Reminders.Remove(recordatorio);
                dbCtx.SaveChanges();

                #region LOG
                //se guarda en la tabla log de la base de datos

                //se crea el objeto log
                Log log = new Log();

                //se le asignan los valores
                log.Date = DateTime.Now;
                log.Description = "Se eliminó recordatorio";

                //se guarda en la base de datos
                dbCtx.Logs.Add(log);
                dbCtx.SaveChanges();

                #endregion

                return RedirectToAction("Recordatorios", "Recordatorios");


            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }


        }
    }
}