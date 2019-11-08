using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TutoriasUTEapp.Areas.Api.Models;

namespace TutoriasUTEapp.Areas.Api.Controllers
{
    public class AndroidMaestrosController : Controller
    {
        public JsonResult Materias(string code, int MaestroID)
        {
            return Json(AndroidMateriasManager.MateriasMaestro(MaestroID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Alumnos(string code, int GrupoID)
        {
            return Json(AndroidAlumnosManager.Alumnos(GrupoID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubirCalificacion(int AlumnoID, int MateriaID, int Unidad, string Calificacion)
        {
            //se crea el objeto
            AndroidCalificaciones calificacion = new AndroidCalificaciones();
            calificacion.AlumnoID = AlumnoID;
            calificacion.MateriaID = MateriaID;
            calificacion.Unidad = Unidad;
            calificacion.Calificacion = Calificacion;

            return Json(AndroidCalificacionesManager.SubirCalificacion(calificacion), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubirComentario(int AlumnoID, int MateriaID, string Comentario)
        {
            //se crea el objeto
            AndroidComentarios comentario = new AndroidComentarios();
            comentario.AlumnoID = AlumnoID;
            comentario.MateriaID = MateriaID;
            comentario.Comentario = Comentario;

            return Json(AndroidComentariosManager.SubirComentario(comentario), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Unidades(string code, int MateriaID)
        {
            return Json(AndroidCalificacionesManager.Unidades(MateriaID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UnidadesUso(string code, int MateriaID)
        {
            return Json(AndroidCalificacionesManager.UnidadesUso(MateriaID), JsonRequestBehavior.AllowGet);
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
