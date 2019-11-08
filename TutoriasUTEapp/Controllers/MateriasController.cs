using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoriasUTEapp.Models;
using TutoriasUTEapp.ViewModels;

namespace TutoriasUTEapp.Controllers
{
    public class MateriasController : Controller
    {
        TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();


        // GET: Materias
        [HttpGet]
        public ActionResult Materias()
        {
            if (Session["Admin"] != null)
            {
                //se crea el viewModel comun
                MateriasCommonViewModel model = new MateriasCommonViewModel();
                model.MateriasCreateVM = new MateriasCreateViewModel();
                model.MateriasResultVM = new MateriasResultViewModel();

                //se guarda la lista
                model.MateriasResultVM.Materias = this.CargarMaterias();



                return View(model);
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult Materias(MateriasCommonViewModel model)
        {
            if (Session["Admin"] != null)
            {
                if (ModelState.IsValid)
                {
                    //se guarda
                    this.GuardarMateria(model.MateriasCreateVM);

                    return RedirectToAction("Materias", "Materias");
                }
                else
                {
                    //se cargan de nuevo
                    model.MateriasResultVM = new MateriasResultViewModel();
                    model.MateriasResultVM.Materias = this.CargarMaterias();

                    return View(model);
                }
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }


        private List<MateriasViewModel> CargarMaterias()
        {
            //se crea la lista
            List<MateriasViewModel> materias = new List<MateriasViewModel>();

            //se buscan las materias
            var query = (from c in dbCtx.Courses
                         join t in dbCtx.Teachers on c.TeacherID equals t.ID
                         orderby c.Description ascending
                         select new
                         {
                             ID = c.ID,
                             Description = c.Description,
                             Units = c.Units,
                             Teacher = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName,
                             TeacherID = t.ID
                         }).ToList();


            //se crean los objetos
            foreach (var c in query)
            {
                //se crea un maestro
                MateriasViewModel materia = new MateriasViewModel();

                //se le asignan los valores actuales
                materia.MateriaID = c.ID;
                materia.Description = c.Description;
                materia.Units = c.Units;
                materia.Teacher = c.Teacher;


                //se agrega a la lista
                materias.Add(materia);

            }//fin del foreach



            return materias;
        }

        private void GuardarMateria(MateriasCreateViewModel nuevaMateria)
        {

            //se guardan los valores del nuevo maestroVM en variables
            string Description = nuevaMateria.Description;
            int Units = nuevaMateria.Units;

            //se busca al maestro
            var queryMaestroID = (from t in dbCtx.Teachers
                                  where t.EmployeeID == nuevaMateria.EmployeeID
                                  select new
                                  {
                                      TeacherID = t.ID
                                  }).SingleOrDefault();


            //si no existe registro con ese ID
            if (queryMaestroID == null)
            {
                RedirectToAction("Materias", "Materias");
            }
            else
            {
                int TeacherID = queryMaestroID.TeacherID;

                //se crea un nuevoa materia
                Course materia = new Course();

                //se le asignan los valores
                materia.Description = Description;
                materia.Units = Units;
                materia.TeacherID = TeacherID;

                //se agrega a la base de datos
                dbCtx.Courses.Add(materia);
                dbCtx.SaveChanges();

                //se guarda en la base de datos el log
                #region LOG
                //se guarda el inicio de sesión en la tabla log de la base de datos

                //se crea el objeto log
                Log log = new Log();

                var queryMaestro = (from t in dbCtx.Teachers
                                    where t.ID == TeacherID
                                    select new
                                    {
                                        TeacherID = t.EmployeeID
                                    }).SingleOrDefault();

                //se le asignan los valores
                log.Date = DateTime.Now;
                log.Description = "Se agregó la materia: " + Description + ", instructor: " + queryMaestro.TeacherID;

                //se guarda en la base de datos
                dbCtx.Logs.Add(log);
                dbCtx.SaveChanges();
                #endregion
            }

        }


        public ActionResult EliminarMateria(int MateriaID)
        {

            if (Session["Admin"] != null)
            {


                //query para guardar que materia se eliminó
                var queryLog = (from c in dbCtx.Courses
                                join t in dbCtx.Teachers on c.TeacherID equals t.ID
                                where c.ID == MateriaID
                                select new
                                {
                                    maestro = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName,
                                    materia = c.Description
                                }).SingleOrDefault();


                //se busca el registro
                var materia = dbCtx.Courses.Where(x => x.ID == MateriaID).SingleOrDefault();
                dbCtx.Courses.Remove(materia);
                dbCtx.SaveChanges();



                #region LOG
                //se guard en la tabla log de la base de datos

                //se crea el objeto log
                Log log = new Log();

                //se le asignan los valores
                log.Date = DateTime.Now;
                log.Description = "Se eliminó materia: " + queryLog.materia + ", impartida por; " + queryLog.maestro;

                //se guarda en la base de datos
                dbCtx.Logs.Add(log);
                dbCtx.SaveChanges();

                #endregion

                return RedirectToAction("Materias", "Materias");


            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }


        }
    }
}