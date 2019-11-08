using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoriasUTEapp.Models;
using TutoriasUTEapp.ViewModels;

namespace TutoriasUTEapp.Controllers
{
    public class MaestrosController : Controller
    {
        TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

        public ActionResult Maestros()
        {
            if (Session["Admin"] != null)
            {
                //se crea el viewModel comun
                MaestrosCommonViewModel model = new MaestrosCommonViewModel();
                model.MaestrosCreateVM = new MaestrosCreateViewModel();
                model.MaestrosResultVM = new MaestrosResultViewModel();

                //se guarda la lista
                model.MaestrosResultVM.Maestros = this.CargarMaestros();

                return View(model);
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult Maestros(MaestrosCommonViewModel model)
        {
            if (Session["Admin"] != null)
            {
                if (ModelState.IsValid)
                {
                    //se guarda
                    this.GuardarMaestro(model.MaestrosCreateVM);

                    return RedirectToAction("Maestros", "Maestros");
                }
                else
                {
                    //se cargan de nuevo
                    model.MaestrosResultVM = new MaestrosResultViewModel();
                    model.MaestrosResultVM.Maestros = this.CargarMaestros();

                    return View(model);
                }
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }

        private List<MaestrosViewModel> CargarMaestros()
        {
            //se crea la lista
            List<MaestrosViewModel> maestros = new List<MaestrosViewModel>();

            //se buscan los maestros
            var query = (from t in dbCtx.Teachers
                             //join tr in dbCtx.TeacherRoles on t.ID equals tr.TeacherID
                         orderby t.LastNameP ascending
                         select new
                         {
                             ID = t.ID,
                             EmployeeID = t.EmployeeID,
                             Name = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName,
                             UserName = t.UserName
                         }
                         ).ToList();

            //se crean los objetos
            foreach (var m in query)
            {
                //se crea un maestro
                MaestrosViewModel maestro = new MaestrosViewModel();

                //se le asignan los valores actuales
                maestro.EmployeeID = m.EmployeeID;
                maestro.Name = m.Name;
                maestro.UserName = m.UserName;

                var queryTutor = (from tr in dbCtx.TeacherRoles
                                  where tr.TeacherID == m.ID && tr.RoleID == 2
                                  select tr).SingleOrDefault();

                //se almacena si es tutor o no
                if (queryTutor != null)
                {
                    maestro.Tutor = true;
                }
                else
                {
                    maestro.Tutor = false;
                }


                //se agrega a la lista
                maestros.Add(maestro);

            }//fin del foreach



            return maestros;
        }

        private void GuardarMaestro(MaestrosCreateViewModel nuevoMaestro)
        {
            //se guardan los valores del nuevo maestroVM en variables
            string EmployeeID = nuevoMaestro.EmployeeID;
            string FirstMidName = nuevoMaestro.FirstMidName;
            string LastNameP = nuevoMaestro.LastNameP;
            string LastNameM = nuevoMaestro.LastNameM;
            string UserName = nuevoMaestro.UserName;

            //un if que busque maestros con el id de empleado, si se encuentra uno que regrese al usuario a Maestros con un redirect to action

            //se crea un nuevo maestro
            Teacher maestro = new Teacher();

            //se le asignan los valores
            maestro.EmployeeID = EmployeeID;
            maestro.FirstMidName = FirstMidName;
            maestro.LastNameP = LastNameP;
            maestro.LastNameM = LastNameM;
            maestro.UserName = UserName;
            maestro.UserPassword = EmployeeID;

            //se agrega a la base de datos
            dbCtx.Teachers.Add(maestro);
            dbCtx.SaveChanges();

            //para agregar los roles
            if (nuevoMaestro.Tutor == true)
            {
                //para obtener el ID del maestro
                var queryID = (from t in dbCtx.Teachers
                               where t.EmployeeID == EmployeeID
                               select new
                               {
                                   ID = t.ID
                               }).SingleOrDefault();

                //se guarda en variable
                int TeacherID = queryID.ID;

                //se crea el rol de maestro
                TeacherRole teacherRoleM = new TeacherRole();

                //se le agregan los valores
                teacherRoleM.TeacherID = TeacherID;
                teacherRoleM.RoleID = 1;

                //se guarda en la base de datos
                dbCtx.TeacherRoles.Add(teacherRoleM);
                dbCtx.SaveChanges();

                //se crea el rol de tutor
                TeacherRole teacherRoleT = new TeacherRole();

                //se le agregan los valores
                teacherRoleT.TeacherID = TeacherID;
                teacherRoleT.RoleID = 2;

                //se guarda en la base de datos
                dbCtx.TeacherRoles.Add(teacherRoleT);
                dbCtx.SaveChanges();
            }
            else
            {
                //si no es tutor solo se agrega como maestro
                //para obtener el ID del maestro
                var queryID = (from t in dbCtx.Teachers
                               where t.EmployeeID == EmployeeID
                               select new
                               {
                                   ID = t.ID
                               }).SingleOrDefault();

                //se guarda en variable
                int TeacherID = queryID.ID;

                //se crea el rol de maestro
                TeacherRole teacherRoleM = new TeacherRole();

                //se le agregan los valores
                teacherRoleM.TeacherID = TeacherID;
                teacherRoleM.RoleID = 1;

                //se guarda en la base de datos
                dbCtx.TeacherRoles.Add(teacherRoleM);
                dbCtx.SaveChanges();
            }

            //se guarda en la base de datos el log
            #region LOG
            //se guarda el inicio de sesión en la tabla log de la base de datos

            //se crea el objeto log
            Log log = new Log();

            //se le asignan los valores
            log.Date = DateTime.Now;
            log.Description = "Se agregó un maestro con ID de Empleado: " + EmployeeID + ", con rol de Tutor: " + nuevoMaestro.Tutor.ToString();

            //se guarda en la base de datos
            dbCtx.Logs.Add(log);
            dbCtx.SaveChanges();

            #endregion
        }

        public ActionResult EliminarMaestro(string EmployeeID)
        {

            if (Session["Admin"] != null)
            {

                //para obtener el ID del maestro
                var queryID = (from t in dbCtx.Teachers
                               where t.EmployeeID == EmployeeID
                               select new
                               {
                                   ID = t.ID
                               }).SingleOrDefault();

                //se guarda en variable
                int TeacherID = queryID.ID;

                //se busca los registros en la tabla de roles que tengan el ID del maestro
                var roles = dbCtx.TeacherRoles.Where(x => x.TeacherID == TeacherID).ToList();
                foreach (var registro in roles)
                {
                    //se borran
                    dbCtx.TeacherRoles.Remove(registro);
                }
                //se guardan los cambios
                dbCtx.SaveChanges();

                //se busca el registro en la tabla de maestro
                var maestro = dbCtx.Teachers.Where(x => x.ID == TeacherID).SingleOrDefault();
                //se borra
                dbCtx.Teachers.Remove(maestro);
                dbCtx.SaveChanges();

                #region LOG
                //se guarda el inicio de sesión en la tabla log de la base de datos

                //se crea el objeto log
                Log log = new Log();

                //se le asignan los valores
                log.Date = DateTime.Now;
                log.Description = "Se eliminó maestro con ID de Empleado: " + EmployeeID;

                //se guarda en la base de datos
                dbCtx.Logs.Add(log);
                dbCtx.SaveChanges();

                #endregion

                return RedirectToAction("Maestros", "Maestros");


            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }


        }
    }
}