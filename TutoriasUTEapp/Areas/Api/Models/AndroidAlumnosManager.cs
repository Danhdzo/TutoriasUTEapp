using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutoriasUTEapp.Models;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidAlumnosManager
    {
        public static List<AndroidAlumnos> Alumnos(int GrupoID)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidAlumnos> alumnos = new List<AndroidAlumnos>();

            //se buscan los alumnos pertenecientes al grupo
            var queryAlumnos = (from cgs in dbCtx.ClassGroupStudents
                                join s in dbCtx.Students on cgs.StudentID equals s.ID
                                where cgs.ClassGroupID == GrupoID
                                orderby s.LastNameP ascending
                                select new
                                {
                                    StudentID = s.ID,
                                    Matricula = s.Registration,
                                    Nombre = s.LastNameP + " " + s.LastNameM + " " + s.FirstMidName
                                }).ToList();

            //por cada uno se agrega un elemento
            foreach(var alumno in queryAlumnos)
            {
                //se crea el objeto 
                AndroidAlumnos objAlumno = new AndroidAlumnos();

                //se agregan los valores
                objAlumno.AlumnoID = alumno.StudentID;
                objAlumno.Matricula = alumno.Matricula;
                objAlumno.Nombre = alumno.Nombre;

                //se agrega a la lista
                alumnos.Add(objAlumno);
            }

            return alumnos;

        }

        public static List<AndroidAlumnos> AlumnosTutor(int MaestroID)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidAlumnos> alumnos = new List<AndroidAlumnos>();

            //se buscan los alumnos pertenecientes al grupo
            var queryAlumnos = (from cgs in dbCtx.ClassGroupStudents
                                join s in dbCtx.Students on cgs.StudentID equals s.ID
                                join cg in dbCtx.ClassGroups on cgs.ClassGroupID equals cg.ID
                                where cg.TeacherID == MaestroID
                                orderby s.LastNameP ascending
                                select new
                                {
                                    StudentID = s.ID,
                                    Matricula = s.Registration,
                                    Nombre = s.LastNameP + " " + s.LastNameM + " " + s.FirstMidName
                                }).ToList();

            //por cada uno se agrega un elemento
            foreach (var alumno in queryAlumnos)
            {
                //se crea el objeto 
                AndroidAlumnos objAlumno = new AndroidAlumnos();

                //se agregan los valores
                objAlumno.AlumnoID = alumno.StudentID;
                objAlumno.Matricula = alumno.Matricula;
                objAlumno.Nombre = alumno.Nombre;

                //se agrega a la lista
                alumnos.Add(objAlumno);
            }

            return alumnos;

        }

        public static List<AndroidAlumno> Alumno(int AlumnoID)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidAlumno> alumno = new List<AndroidAlumno>();

            //se busca el alumno
            var queryAlumno = (from s in dbCtx.Students
                               join st in dbCtx.Situations on s.SituationID equals st.ID
                               where s.ID == AlumnoID
                               select new
                               {
                                   registration = s.Registration,
                                   firstName = s.FirstMidName,
                                   lastNameP = s.LastNameP,
                                   lastNameM = s.LastNameM,
                                   tel = s.Telephone,
                                   emTel = s.EmergencyTelephone,
                                   academicEmail = s.AcademicEmail,
                                   birthdate = s.Birthday,
                                   situation = st.Description
                               }).SingleOrDefault();

            if (queryAlumno != null)
            {
                //se guarda el objto
                AndroidAlumno alumnomodel = new AndroidAlumno();

                //se asignan los valores
                alumnomodel.Registration = queryAlumno.registration;
                alumnomodel.FirstMidName = queryAlumno.firstName;
                alumnomodel.LastNameP = queryAlumno.lastNameP;
                alumnomodel.LastNameM = queryAlumno.lastNameM;
                alumnomodel.Telephone = queryAlumno.tel;
                alumnomodel.EmergencyTelephone = queryAlumno.emTel;
                alumnomodel.AcademicEmail = queryAlumno.academicEmail;
                alumnomodel.Birthday = queryAlumno.birthdate;
                alumnomodel.Situation = queryAlumno.situation;

                //se agrega a la lista
                alumno.Add(alumnomodel);
            }

            return alumno;
        }
    }
}