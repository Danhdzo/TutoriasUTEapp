using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutoriasUTEapp.Models;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidCalificacionesManager
    {

        public static List<AndroidCalificacionesReturn> SubirCalificacion(AndroidCalificaciones calificacion)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidCalificacionesReturn> califRet = new List<AndroidCalificacionesReturn>();

            //se crea el objeto para subir
            StudentCourseGrade newGrade = new StudentCourseGrade();

            //se le agregan los valores
            newGrade.StudentID = calificacion.AlumnoID;
            newGrade.CourseID = calificacion.MateriaID;
            newGrade.Unit = calificacion.Unidad;
            newGrade.Grade = calificacion.Calificacion;
            newGrade.Date = DateTime.Now;

            try
            {
                //se agrega a la base de datos
                dbCtx.StudentCourseGrades.Add(newGrade);
                dbCtx.SaveChanges();

                #region LOG
                Log log = new Log();

                //se le asignan los valores
                log.Date = DateTime.Now;
                log.Description = "Se subió calificación para alumno con ID: " + calificacion.AlumnoID + ", de la materia con ID: " + calificacion.MateriaID;

                //se guarda en la base de datos
                dbCtx.Logs.Add(log);
                dbCtx.SaveChanges();

                #endregion

                //se regresa el objeto
                AndroidCalificacionesReturn objCalifRet = new AndroidCalificacionesReturn();
                objCalifRet.Status = true;

                califRet.Add(objCalifRet);
            }
            catch (Exception ex)
            {
                #region LOG
                Log log = new Log();

                //se le asignan los valores
                log.Date = DateTime.Now;
                log.Description = "Hubo un erro para alumno con ID: " + calificacion.AlumnoID + ", de la materia con ID: " + calificacion.MateriaID + " -" + ex.Message;

                //se guarda en la base de datos
                dbCtx.Logs.Add(log);
                dbCtx.SaveChanges();

                #endregion

                //se regresa el objeto
                AndroidCalificacionesReturn objCalifRet = new AndroidCalificacionesReturn();
                objCalifRet.Status = false;

                califRet.Add(objCalifRet);
            }

            return califRet;
        }

        public static List<AndroidCalificacionesUnidades> Unidades(int MateriaID)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidCalificacionesUnidades> unitsReturn = new List<AndroidCalificacionesUnidades>();

            var query = (from c in dbCtx.Courses
                         where c.ID == MateriaID
                         select new
                         {
                             Unidades = c.Units
                         }).SingleOrDefault();


            AndroidCalificacionesUnidades unidades = new AndroidCalificacionesUnidades();
            unidades.Unidades = query.Unidades;

            unitsReturn.Add(unidades);

            return unitsReturn;
        }

        public static List<AndroidCalificacionesUnidadesUso> UnidadesUso(int MateriaID)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidCalificacionesUnidadesUso> unitsReturn = new List<AndroidCalificacionesUnidadesUso>();

            var query = (from scg in dbCtx.StudentCourseGrades
                         where scg.CourseID == MateriaID
                         group scg by scg.Unit into scgg
                         select new
                         {
                             Unidad = scgg.FirstOrDefault().Unit
                         }).ToList();

            if (query.Count != 0)
            {
                foreach (var unit in query)
                {
                    AndroidCalificacionesUnidadesUso unidad = new AndroidCalificacionesUnidadesUso();
                    unidad.Unidad = unit.Unidad;

                    unitsReturn.Add(unidad);
                }
            }
            else
            {
                AndroidCalificacionesUnidadesUso unidad = new AndroidCalificacionesUnidadesUso();
                unidad.Unidad = 0;

                unitsReturn.Add(unidad);
            }

            return unitsReturn;
        }


        public static List<AndroidCalificacionesAlumno> CalificacionesAlumno(int AlumnoID, int MateriaID)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidCalificacionesAlumno> calificacionesRet = new List<AndroidCalificacionesAlumno>();

            var query = (from scg in dbCtx.StudentCourseGrades
                         where scg.CourseID == MateriaID
                         where scg.StudentID == AlumnoID
                         select new
                         {
                             Unidad = scg.Unit,
                             Calif = scg.Grade
                         }).ToList();

            foreach (var calif in query)
            {
                AndroidCalificacionesAlumno calificacion = new AndroidCalificacionesAlumno();
                calificacion.Unidad = calif.Unidad;
                calificacion.Calificacion = calif.Calif;

                calificacionesRet.Add(calificacion);
            }

            return calificacionesRet;
        }
    }
}