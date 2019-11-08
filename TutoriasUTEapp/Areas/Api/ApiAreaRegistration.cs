using System.Web.Mvc;

namespace TutoriasUTEapp.Areas.Api
{
    public class ApiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Api";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Api_default",
                "Api/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            //LOGIN
            context.MapRoute(
                "AccesoLogin",
                "Api/AndroidLogin/Login/{us}/{pass}/{role}",
                new { controller = "AndroidLogin", action = "Login", us = "", pass = "", role = "" }
            );

            //MAESTRO
            context.MapRoute(
                "AccesoMateriasMaestro",
                "Api/AndroidMaestros/Materias/{code}/{MaestroID}",
                new { controller = "AndroidMaestros", action = "Materias", code = "", MaestroID = "" }
            );
            context.MapRoute(
                "AccesoAlumnosMaestro",
                "Api/AndroidMaestros/Alumnos/{code}/{GrupoID}",
                new { controller = "AndroidMaestros", action = "Alumnos", code = "", GrupoID = "" }
            );
            context.MapRoute(
                "AccesoSubirCalificacionMaestro",
                "Api/AndroidMaestros/SubirCalificacion/{AlumnoID}/{MateriaID}/{Unidad}/{Calificacion}",
                new { controller = "AndroidMaestros", action = "SubirCalificacion", AlumnoID = "", MateriaID = "", Unidad = "", Calificacion = "" }
            );
            context.MapRoute(
                "AccesoSubirComentarioMaestro",
                "Api/AndroidMaestros/SubirComentario/{AlumnoID}/{MateriaID}/{Comentario}",
                new { controller = "AndroidMaestros", action = "SubirComentario", AlumnoID = "", MateriaID = "", Comentario = "" }
            );
            context.MapRoute(
                "AccesoUnidadesMaestro",
                "Api/AndroidMaestros/Unidades/{code}/{MateriaID}",
                new { controller = "AndroidMaestros", action = "Unidades", code = "", MateriaID = "" }
            );
            context.MapRoute(
                "AccesoUnidadesUsoMaestro",
                "Api/AndroidMaestros/UnidadesUso/{code}/{MateriaID}",
                new { controller = "AndroidMaestros", action = "UnidadesUso", code = "", MateriaID = "" }
            );
            context.MapRoute(
                "AccesoCalificacionesAlumnoMaestro",
                "Api/AndroidMaestros/CalificacionesAlumno/{AlumnoID}/{MateriaID}",
                new { controller = "AndroidMaestros", action = "CalificacionesAlumno", AlumnoID = "", MateriaID = "" }
            );
            context.MapRoute(
                "AccesoComentariosAlumnoMaestro",
                "Api/AndroidMaestros/ComentariosAlumno/{AlumnoID}/{MateriaID}",
                new { controller = "AndroidMaestros", action = "ComentariosAlumno", AlumnoID = "", MateriaID = "" }
            );

            //TUTOR
            context.MapRoute(
                "AccesoMateriasTutor",
                "Api/AndroidTutores/Materias/{code}/{MaestroID}",
                new { controller = "AndroidTutores", action = "Materias", code = "", MaestroID = "" }
            );
            context.MapRoute(
                "AccesoAlumnosTutor",
                "Api/AndroidTutores/Alumnos/{code}/{MaestroID}",
                new { controller = "AndroidTutores", action = "Alumnos", code = "", MaestroID = "" }
            );
            context.MapRoute(
                "AccesoAlumnoIndividualTutor",
                "Api/AndroidTutores/Alumno/{code}/{AlumnoID}",
                new { controller = "AndroidTutores", action = "Alumno", code = "", AlumnoID = "" }
            );
            context.MapRoute(
                "AccesoCalificacionesAlumnoTutor",
                "Api/AndroidTutores/CalificacionesAlumno/{AlumnoID}/{MateriaID}",
                new { controller = "AndroidTutores", action = "CalificacionesAlumno", AlumnoID = "", MateriaID = "" }
            );
            context.MapRoute(
                "AccesoComentariosAlumnoTutor",
                "Api/AndroidTutores/ComentariosAlumno/{AlumnoID}/{MateriaID}",
                new { controller = "AndroidTutores", action = "ComentariosAlumno", AlumnoID = "", MateriaID = "" }
            );

            //RECORDATORIOS
            context.MapRoute(
                "AccesoRecordatorios",
                "Api/AndroidRecordatorios/Recordatorios/{code}/{role}",
                new { controller = "AndroidRecordatorios", action = "Recordatorios", code = "", role = "" }
            );
        }
    }
}