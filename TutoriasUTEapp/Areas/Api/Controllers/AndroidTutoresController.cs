using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoriasUTEapp.Areas.Api.Models;

namespace TutoriasUTEapp.Areas.Api.Controllers
{
    public class AndroidTutoresController : Controller
    {
        public JsonResult Materias(string code, int MaestroID)
        {
            return Json(AndroidMateriasManager.MateriasTutor(MaestroID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Alumnos(string code, int MaestroID)
        {
            return Json(AndroidAlumnosManager.AlumnosTutor(MaestroID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Alumno(string code, int AlumnoID)
        {
            return Json(AndroidAlumnosManager.Alumno(AlumnoID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalificacionesAlumno(int AlumnoID, int MateriaID)
        {
            return Json(AndroidCalificacionesManager.CalificacionesAlumno(AlumnoID, MateriaID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ComentariosAlumno(int AlumnoID, int MateriaID)
        {
            return Json(AndroidComentariosManager.ComentariosAlumno(AlumnoID, MateriaID), JsonRequestBehavior.AllowGet);
        }
    }
}
