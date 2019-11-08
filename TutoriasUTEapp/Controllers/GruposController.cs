using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoriasUTEapp.Models;
using TutoriasUTEapp.ViewModels;

namespace TutoriasUTEapp.Controllers
{
    public class GruposController : Controller
    {

        TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

        public ActionResult Grupos()
        {
            if (Session["Admin"] != null)
            {
                //OBTENER Grupos 
                // se buscan los grupos
                var queryGrupos = (from cg in dbCtx.ClassGroups
                                   join t in dbCtx.Teachers on cg.TeacherID equals t.ID
                                   select new
                                   {
                                       ID = cg.ID,
                                       GroupID = cg.GroupID,
                                       Tutor = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName
                                   }).ToList();

                //lista para almacenar los grupos
                List<GruposViewModel> grupos = new List<GruposViewModel>();

                //por cada grupo se agrega un elemento a la lista
                foreach (var grupo in queryGrupos)
                {
                    //se crea un gruposViewModel
                    GruposViewModel grupovm = new GruposViewModel();

                    //se le agregan los valores
                    grupovm.ID = grupo.ID;
                    grupovm.GroupID = grupo.GroupID;
                    grupovm.Tutor = grupo.Tutor;

                    grupos.Add(grupovm);
                }

                //se regresa la lista
                return View(grupos);
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public ActionResult CrearGrupo()
        {
            if (Session["Admin"] != null)
            {
                //se crea un CrearGrupoViewModel
                GruposCrearViewModel grupo = new GruposCrearViewModel();

                #region Se guardan las listas

                //LISTA DE CARRERAS
                //se obtienen
                var queryCarreras = (from c in dbCtx.Careers select c).ToList();

                //se crea lista y se agregan
                List<Career> carreras = new List<Career>(queryCarreras);

                //se agregan al viewmodel
                grupo.Carreras = carreras;


                //LISTA DE MODALIDADES
                //se obtienen
                var queryModalidades = (from m in dbCtx.Modalities select m).ToList();

                //se crea lista y se agregan
                List<Modality> modalidades = new List<Modality>(queryModalidades);

                //se agregan al viewmodel
                grupo.Modalidades = modalidades;


                //LISTA DE TURNOS
                //se obtienen
                var queryTurnos = (from t in dbCtx.Turns select t).ToList();

                //se crea lista y se agregan
                List<Turn> turnos = new List<Turn>(queryTurnos);

                //se agregan al viewmodel
                grupo.Turnos = turnos;

                //LISTA DE TUTORES
                var queryTutores = (from t in dbCtx.Teachers
                                    join tr in dbCtx.TeacherRoles on t.ID equals tr.TeacherID
                                    where tr.RoleID == 2
                                    select new
                                    {
                                        ID = t.ID,
                                        Nombre = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName
                                    }).ToList();

                //se crea lista
                List<GruposCrearTutorViewModel> tutores = new List<GruposCrearTutorViewModel>();

                //por cada tutor se crean los vm
                foreach (var tutor in queryTutores)
                {
                    //se verifica cuales se utilizan
                    var queryTutor = (from cg in dbCtx.ClassGroups
                                      where cg.TeacherID == tutor.ID
                                      select cg).SingleOrDefault();

                    //si el tutor ya tiene grupo no se muestra
                    if (queryTutor == null)
                    {
                        GruposCrearTutorViewModel tutorvm = new GruposCrearTutorViewModel();
                        tutorvm.ID = tutor.ID;
                        tutorvm.Nombre = tutor.Nombre;

                        tutores.Add(tutorvm);
                    }

                }

                //se agregan al viewmodel
                grupo.Tutores = tutores;

                #endregion

                //se regresa la vista
                return View(grupo);
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult CrearGrupo(GruposCrearViewModel nuevoGrupo)
        {
            if (Session["Admin"] != null)
            {
                if (ModelState.IsValid)
                {
                    //se guardan en variables los valores
                    int Carrera = nuevoGrupo.CareerID;
                    int Cuatrimestre = nuevoGrupo.Term;
                    string Seccion = nuevoGrupo.Section.ToUpper();
                    int Modalidad = nuevoGrupo.ModalityID;
                    int Turno = nuevoGrupo.TurnID;
                    int Generacion = nuevoGrupo.Generation;
                    int TutorID = nuevoGrupo.TutorID;

                    #region CREAR GroupID

                    //Abreviacion de carrera
                    var querycarrea = (from c in dbCtx.Careers
                                       where c.ID == Carrera
                                       select new
                                       {
                                           carrera = c.Abbreviation
                                       }).SingleOrDefault();

                    //Modalidad
                    var querymodalidad = (from m in dbCtx.Modalities
                                          where m.ID == Modalidad
                                          select new
                                          {
                                              modalidad = m.Description
                                          }).SingleOrDefault();

                    //Turno
                    var queryturno = (from t in dbCtx.Turns
                                      where t.ID == Turno
                                      select new
                                      {
                                          turno = t.Description
                                      }).SingleOrDefault();

                    string TurnoAb = null;
                    if (queryturno.turno.ToString() == "Matutino")
                    {
                        TurnoAb = "MAT";
                    }
                    else
                    {
                        TurnoAb = "NOC";
                    }

                    //TICSI UTE 4 A BIS MAT G49
                    string GroupID = querycarrea.carrera.ToString() + " UTE "
                                    + Cuatrimestre.ToString() + Seccion + " "
                                    + querymodalidad.modalidad.ToString() + " "
                                    + TurnoAb + " "
                                    + "G" + Generacion.ToString();


                    #endregion


                    //se crea aun ClassGroup
                    ClassGroup grupo = new ClassGroup();

                    //se guardan los valores
                    grupo.GroupID = GroupID;
                    grupo.CareerID = Carrera;
                    grupo.Term = Cuatrimestre;
                    grupo.Section = Seccion;
                    grupo.ModalityID = Modalidad;
                    grupo.TurnID = Turno;
                    grupo.Generation = Generacion;
                    grupo.TeacherID = TutorID;

                    //se guarda
                    dbCtx.ClassGroups.Add(grupo);
                    dbCtx.SaveChanges();

                    #region LOG
                    //se guarda el grupo creadoen la tabla log de la base de datos

                    //se crea el objeto log
                    Log log = new Log();

                    //se le asignan los valores
                    log.Date = DateTime.Now;
                    log.Description = "Se agregó un grupo con ID: " + GroupID + ", con ID de Tutor: " + TutorID;

                    //se guarda en la base de datos
                    dbCtx.Logs.Add(log);
                    dbCtx.SaveChanges();

                    #endregion


                    //se obtiene el ID
                    var queryID = (from cg in dbCtx.ClassGroups
                                   where cg.GroupID == GroupID
                                   select new
                                   {
                                       ID = cg.ID
                                   }).SingleOrDefault();

                    int id = queryID.ID;
                    //se debe pasar el id del grupo
                    return RedirectToAction("AgregarAlumnos", "Grupos", new { ID = id });
                }
                else
                {
                    #region Se guardan las listas

                    //LISTA DE CARRERAS
                    //se obtienen
                    var queryCarreras = (from c in dbCtx.Careers select c).ToList();

                    //se crea lista y se agregan
                    List<Career> carreras = new List<Career>(queryCarreras);

                    //se agregan al viewmodel
                    nuevoGrupo.Carreras = carreras;


                    //LISTA DE MODALIDADES
                    //se obtienen
                    var queryModalidades = (from m in dbCtx.Modalities select m).ToList();

                    //se crea lista y se agregan
                    List<Modality> modalidades = new List<Modality>(queryModalidades);

                    //se agregan al viewmodel
                    nuevoGrupo.Modalidades = modalidades;


                    //LISTA DE TURNOS
                    //se obtienen
                    var queryTurnos = (from t in dbCtx.Turns select t).ToList();

                    //se crea lista y se agregan
                    List<Turn> turnos = new List<Turn>(queryTurnos);

                    //se agregan al viewmodel
                    nuevoGrupo.Turnos = turnos;

                    //LISTA DE TUTORES
                    var queryTutores = (from t in dbCtx.Teachers
                                        join tr in dbCtx.TeacherRoles on t.ID equals tr.TeacherID
                                        where tr.RoleID == 2
                                        select new
                                        {
                                            ID = t.ID,
                                            Nombre = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName
                                        }).ToList();

                    //se crea lista
                    List<GruposCrearTutorViewModel> tutores = new List<GruposCrearTutorViewModel>();

                    //por cada tutor se crean los vm
                    foreach (var tutor in queryTutores)
                    {
                        GruposCrearTutorViewModel tutorvm = new GruposCrearTutorViewModel();
                        tutorvm.ID = tutor.ID;
                        tutorvm.Nombre = tutor.Nombre;

                        tutores.Add(tutorvm);

                    }

                    //se agregan al viewmodel
                    nuevoGrupo.Tutores = tutores;

                    #endregion
                    return View(nuevoGrupo);
                }
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }


        [HttpGet]
        public ActionResult AgregarAlumnos(int ID)
        {
            if (Session["Admin"] != null)
            {
                //se crea el viewModel comun
                GruposCrearAlumnosCommonViewModel model = new GruposCrearAlumnosCommonViewModel();
                model.GruposCrearAlumnosCreateVM = new GruposCrearAlumnosCreateViewModel();
                model.GruposCrearAlumnosResultVM = new GruposCrearAlumnosResultViewModel();
                model.GrupoID = ID;


                #region Cargar Alumnos
                //se crea la lista 
                List<GruposCrearAlumnosViewModel> alumnos = new List<GruposCrearAlumnosViewModel>();

                //se buscan
                var queryAlumnos = (from s in dbCtx.Students
                                    join cgs in dbCtx.ClassGroupStudents on s.ID equals cgs.StudentID
                                    where cgs.ClassGroupID == ID
                                    orderby s.LastNameP ascending
                                    select new
                                    {
                                        ID = s.ID,
                                        Matricula = s.Registration,
                                        Nombre = s.LastNameP + " " + s.LastNameM + " " + s.FirstMidName
                                    }).ToList();

                foreach (var alumno in queryAlumnos)
                {
                    //se crea el alumno
                    GruposCrearAlumnosViewModel alumnovm = new GruposCrearAlumnosViewModel();

                    //se le asignan los valores
                    alumnovm.ID = alumno.ID;
                    alumnovm.Registration = alumno.Matricula;
                    alumnovm.Name = alumno.Nombre;

                    //se agrega a la lista
                    alumnos.Add(alumnovm);
                }
                #endregion

                #region Listas
                //LISTA DE Situaciones
                //se obtienen
                var querysituaciones = (from s in dbCtx.Situations select s).ToList();

                //se crea lista y se agregan
                List<Situation> situaciones = new List<Situation>(querysituaciones);

                //se agregan al viewmodel
                model.GruposCrearAlumnosCreateVM.Situaciones = situaciones;
                #endregion

                //se guarda la lista
                model.GruposCrearAlumnosResultVM.Alumnos = alumnos;

                return View(model);

            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult AgregarAlumnos(GruposCrearAlumnosCommonViewModel model)
        {
            if (Session["Admin"] != null)
            {
                if (ModelState.IsValid)
                {
                    //se guarda
                    #region Guardar Alumno

                    //se guardan los valores
                    string Registration = model.GruposCrearAlumnosCreateVM.Registration;
                    string FirstMidName = model.GruposCrearAlumnosCreateVM.FirstMidName;
                    string LastNameP = model.GruposCrearAlumnosCreateVM.LastNameP;
                    string LastNameM = model.GruposCrearAlumnosCreateVM.LastNameM;
                    string Telephone = model.GruposCrearAlumnosCreateVM.Telephone;
                    string EmerTelephone = model.GruposCrearAlumnosCreateVM.EmergencyTelephone;
                    string AcademicEmail = model.GruposCrearAlumnosCreateVM.AcademicEmail;
                    string Birthdat = model.GruposCrearAlumnosCreateVM.Birthday;
                    int SituationID = model.GruposCrearAlumnosCreateVM.SituationID;


                    //se crea un nuevo estudiante
                    Student student = new Student();
                    student.Registration = Registration;
                    student.FirstMidName = FirstMidName;
                    student.LastNameP = LastNameP;
                    student.LastNameM = LastNameM;
                    student.Telephone = Telephone;
                    student.EmergencyTelephone = EmerTelephone;
                    student.AcademicEmail = AcademicEmail;
                    student.Birthday = Birthdat;
                    student.SituationID = SituationID;

                    //se agrega a la base de datos
                    dbCtx.Students.Add(student);
                    dbCtx.SaveChanges();

                    //SE GUARDA CON EL GRUPO
                    ClassGroupStudent cgStudent = new ClassGroupStudent();

                    //se obtiene el id
                    var queryid = (from s in dbCtx.Students
                                   where s.Registration == Registration
                                   select new
                                   {
                                       id = s.ID
                                   }).SingleOrDefault();

                    //se agregan valores
                    cgStudent.StudentID = queryid.id;
                    cgStudent.ClassGroupID = model.GrupoID;

                    //se guardan en base de datos
                    dbCtx.ClassGroupStudents.Add(cgStudent);
                    dbCtx.SaveChanges();

                    #endregion

                    #region LOG
                    //se guarda ealumno en la tabla log de la base de datos

                    //se crea el objeto log
                    Log log = new Log();

                    //se le asignan los valores
                    log.Date = DateTime.Now;
                    log.Description = "Se agregó un alumno al grupo: " + model.GrupoID + ", con matricula: " + Registration;

                    //se guarda en la base de datos
                    dbCtx.Logs.Add(log);
                    dbCtx.SaveChanges();

                    #endregion

                    //regresa al metodo get
                    return RedirectToAction("AgregarAlumnos", "Grupos", model.GrupoID);
                }
                else
                {
                    //se cargan de nuevo
                    model.GruposCrearAlumnosResultVM = new GruposCrearAlumnosResultViewModel();

                    #region Cargar Alumnos
                    //se crea la lista 
                    List<GruposCrearAlumnosViewModel> alumnos = new List<GruposCrearAlumnosViewModel>();

                    //se buscan
                    var queryAlumnos = (from s in dbCtx.Students
                                        join cgs in dbCtx.ClassGroupStudents on s.ID equals cgs.StudentID
                                        where cgs.ClassGroupID == model.GrupoID
                                        orderby s.LastNameP ascending
                                        select new
                                        {
                                            ID = s.ID,
                                            Matricula = s.Registration,
                                            Nombre = s.LastNameP + " " + s.LastNameM + " " + s.FirstMidName
                                        }).ToList();

                    foreach (var alumno in queryAlumnos)
                    {
                        //se crea el alumno
                        GruposCrearAlumnosViewModel alumnovm = new GruposCrearAlumnosViewModel();

                        //se le asignan los valores
                        alumnovm.ID = alumno.ID;
                        alumnovm.Registration = alumno.Matricula;
                        alumnovm.Name = alumno.Nombre;

                        //se agrega a la lista
                        alumnos.Add(alumnovm);
                    }
                    #endregion

                    #region Listas
                    //LISTA DE Situaciones
                    //se obtienen
                    var querysituaciones = (from s in dbCtx.Situations select s).ToList();

                    //se crea lista y se agregan
                    List<Situation> situaciones = new List<Situation>(querysituaciones);

                    //se agregan al viewmodel
                    model.GruposCrearAlumnosCreateVM.Situaciones = situaciones;
                    #endregion

                    model.GruposCrearAlumnosResultVM.Alumnos = alumnos;


                    return View(model);
                }

                //manda a la vista de agregar materias que va a ser una lista que almacene el id de materia, nombre, instructor y si es vd o f para agregarla
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }


        [HttpGet]
        public ActionResult AgregarMaterias(int ID)
        {
            if (Session["Admin"] != null)
            {

                //se crea la lista
                List<GruposCrearMateriasViewModel> materias = new List<GruposCrearMateriasViewModel>();

                //se buscan las materias a agregar
                var queryMaterias = (from c in dbCtx.Courses
                                     join t in dbCtx.Teachers on c.TeacherID equals t.ID
                                     orderby c.Description ascending
                                     select new
                                     {
                                         ID = c.ID,
                                         Desc = c.Description,
                                         Maestro = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName
                                     }).ToList();

                //se agregan a la lista
                foreach (var materia in queryMaterias)
                {
                    GruposCrearMateriasViewModel materiavm = new GruposCrearMateriasViewModel();

                    materiavm.ID = materia.ID;
                    materiavm.Description = materia.Desc;
                    materiavm.Maestro = materia.Maestro;
                    materiavm.GrupoID = ID;

                    materias.Add(materiavm);
                }


                return View(materias.ToList());
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult AgregarMaterias(List<GruposCrearMateriasViewModel> materias)
        {
            if (Session["Admin"] != null)
            {

                foreach (var materia in materias)
                {

                    //se verifica si esta seleccionada
                    if (materia.isSelected == true)
                    {
                        ClassGroupCourse cgCourse = new ClassGroupCourse();

                        //se guardan los valores
                        cgCourse.ClassGroupID = materia.GrupoID;
                        cgCourse.CourseID = materia.ID;

                        //se guardan
                        dbCtx.ClassGroupCourses.Add(cgCourse);
                        dbCtx.SaveChanges();

                        #region LOG
                        //se guardan las materias del grupo en la tabla log de la base de datos

                        //se crea el objeto log
                        Log log = new Log();

                        //se le asignan los valores
                        log.Date = DateTime.Now;
                        log.Description = "Se agregó una materia con ID: " + materia.ID + "al grupo con ID: " + materia.GrupoID;

                        //se guarda en la base de datos
                        dbCtx.Logs.Add(log);
                        dbCtx.SaveChanges();

                        #endregion
                    }
                }



                return RedirectToAction("Grupos", "Grupos");
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }


        public ActionResult VerGrupo(int ID)
        {
            if (Session["Admin"] != null)
            {
                //se crea el viewModel comun
                GruposVerCommonViewModel model = new GruposVerCommonViewModel();

                #region Grupo
                //se crea el grupo
                var queryGrupo = (from cg in dbCtx.ClassGroups
                                  join t in dbCtx.Teachers on cg.TeacherID equals t.ID
                                  join c in dbCtx.Careers on cg.CareerID equals c.ID
                                  join tu in dbCtx.Turns on cg.TurnID equals tu.ID
                                  join m in dbCtx.Modalities on cg.ModalityID equals m.ID
                                  where cg.ID == ID
                                  select new
                                  {
                                      GrupoID = cg.GroupID,
                                      Carrera = c.Description,
                                      Cuatrimestre = cg.Term,
                                      Seccion = cg.Section,
                                      Modalidad = m.Description,
                                      Turno = tu.Description,
                                      Generacion = cg.Generation,
                                      Tutor = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName
                                  }).SingleOrDefault();

                //se crea el objeto grupo
                GruposVerGrupoViewModel grupovm = new GruposVerGrupoViewModel();
                grupovm.GrupoID = queryGrupo.GrupoID;
                grupovm.ID = ID;
                grupovm.Carrera = queryGrupo.Carrera;
                grupovm.Cuatrimestre = queryGrupo.Cuatrimestre;
                grupovm.Seccion = queryGrupo.Seccion;
                grupovm.Modalidad = queryGrupo.Modalidad;
                grupovm.Turno = queryGrupo.Turno;
                grupovm.Generacion = queryGrupo.Generacion;
                grupovm.Tutor = queryGrupo.Tutor;

                //se guarda en el modelo
                model.Grupo = grupovm;
                #endregion

                #region Cargar Alumnos
                //se crea la lista 
                List<GruposVerAlumnosViewModel> alumnos = new List<GruposVerAlumnosViewModel>();

                //se buscan
                var queryAlumnos = (from s in dbCtx.Students
                                    join cgs in dbCtx.ClassGroupStudents on s.ID equals cgs.StudentID
                                    where cgs.ClassGroupID == ID
                                    orderby s.LastNameP ascending
                                    select new
                                    {
                                        ID = s.ID,
                                        Matricula = s.Registration,
                                        Nombre = s.LastNameP + " " + s.LastNameM + " " + s.FirstMidName
                                    }).ToList();

                foreach (var alumno in queryAlumnos)
                {
                    //se crea el alumno
                    GruposVerAlumnosViewModel alumnovm = new GruposVerAlumnosViewModel();

                    //se le asignan los valores
                    alumnovm.ID = alumno.ID;
                    alumnovm.Registration = alumno.Matricula;
                    alumnovm.Name = alumno.Nombre;

                    //se agrega a la lista
                    alumnos.Add(alumnovm);
                }

                //se agrega
                model.Alumnos = alumnos;
                #endregion

                #region Materias
                //se crea la lista
                List<GruposVerMateriasViewModel> materias = new List<GruposVerMateriasViewModel>();

                //se buscan las materias a agregar
                var queryMaterias = (from cgc in dbCtx.ClassGroupCourses
                                     join c in dbCtx.Courses on cgc.CourseID equals c.ID
                                     join cg in dbCtx.ClassGroups on cgc.ClassGroupID equals cg.ID
                                     join t in dbCtx.Teachers on c.TeacherID equals t.ID
                                     where cgc.ClassGroupID == ID
                                     orderby c.Description ascending
                                     select new
                                     {
                                         Desc = c.Description,
                                         Maestro = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName
                                     }).ToList();

                //se agregan a la lista
                foreach (var materia in queryMaterias)
                {
                    GruposVerMateriasViewModel materiavm = new GruposVerMateriasViewModel();

                    materiavm.Description = materia.Desc;
                    materiavm.Maestro = materia.Maestro;

                    materias.Add(materiavm);
                }

                //se agregan
                model.Materias = materias;
                #endregion


                return View(model);
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult EliminarGrupo(int ID)
        {
            if (Session["Admin"] != null)
            {
                //se borran los registros de lla tabla de grupo y materias
                var queryCGC = (from cgc in dbCtx.ClassGroupCourses
                                where cgc.ClassGroupID == ID
                                select cgc).ToList();
                foreach (var cgc in queryCGC)
                {
                    dbCtx.ClassGroupCourses.Remove(cgc);
                    dbCtx.SaveChanges();
                }

                //se obtienen los registros de estudiantes y de la tabla de grupo students
                var queryCGS = (from cgs in dbCtx.ClassGroupStudents
                                where cgs.ClassGroupID == ID
                                select cgs).ToList();
                var querySt = (from s in dbCtx.Students
                               join cgs in dbCtx.ClassGroupStudents on s.ID equals cgs.StudentID
                               where cgs.ClassGroupID == ID
                               select s).ToList();

                //primero se borra la tabla muchos a muchos
                foreach (var cgs in queryCGS)
                {
                    dbCtx.ClassGroupStudents.Remove(cgs);
                    dbCtx.SaveChanges();
                }
                foreach (var s in querySt)
                {
                    dbCtx.Students.Remove(s);
                    dbCtx.SaveChanges();
                }

                //se borra el grupo
                var queryGr = (from cg in dbCtx.ClassGroups
                               where cg.ID == ID
                               select cg).SingleOrDefault();

                //se guarda el grupo para el log
                string GroupID = queryGr.GroupID;

                dbCtx.ClassGroups.Remove(queryGr);
                dbCtx.SaveChanges();

                #region LOG
                //se guarda el inicio de sesión en la tabla log de la base de datos

                //se crea el objeto log
                Log log = new Log();

                //se le asignan los valores
                log.Date = DateTime.Now;
                log.Description = "Se eliminó el grupo con ID de grupo: " + GroupID + " con todos sus datos de alumnos y materias";

                //se guarda en la base de datos
                dbCtx.Logs.Add(log);
                dbCtx.SaveChanges();

                #endregion

                //se regresa a grupos
                return RedirectToAction("Grupos", "Grupos");
            }
            else
            {
                //si no se inicio sesion no se puede acceder a esta pagina
                return RedirectToAction("Login", "Login");
            }
        }
    }
}
