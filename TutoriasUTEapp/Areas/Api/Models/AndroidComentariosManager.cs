using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutoriasUTEapp.Models;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidComentariosManager
    {
        public static List<AndroidComentariosReturn> SubirComentario(AndroidComentarios comentario)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidComentariosReturn> commentRet = new List<AndroidComentariosReturn>();

            //se crea el objeto para subir
            StudentCourseComment newComment = new StudentCourseComment();

            //se le agregan los valores
            newComment.StudentID = comentario.AlumnoID;
            newComment.CourseID = comentario.MateriaID;
            newComment.Description = comentario.Comentario;
            newComment.Date = DateTime.Now;

            try
            {
                //se agrega a la base de datos
                dbCtx.StudentCourseComments.Add(newComment);
                dbCtx.SaveChanges();

                #region LOG
                Log log = new Log();

                //se le asignan los valores
                log.Date = DateTime.Now;
                log.Description = "Se subió comentario para alumno con ID: " + comentario.AlumnoID + ", de la materia con ID: " + comentario.MateriaID;

                //se guarda en la base de datos
                dbCtx.Logs.Add(log);
                dbCtx.SaveChanges();

                #endregion


                //se regresa el objeto
                AndroidComentariosReturn objCommentRet = new AndroidComentariosReturn();
                objCommentRet.Status = true;

                commentRet.Add(objCommentRet);


            }
            catch (Exception ex)
            {
                #region LOG
                Log log = new Log();

                //se le asignan los valores
                log.Date = DateTime.Now;
                log.Description = "Hubo un erro para alumno con ID: " + comentario.AlumnoID + ", de la materia con ID: " + comentario.MateriaID + " -" + ex.Message;

                //se guarda en la base de datos
                dbCtx.Logs.Add(log);
                dbCtx.SaveChanges();

                #endregion

                //se regresa el objeto
                AndroidComentariosReturn objCommentRet = new AndroidComentariosReturn();
                objCommentRet.Status = false;

                commentRet.Add(objCommentRet);
            }

            return commentRet;
        }

        public static List<AndroidComentariosAlumno> ComentariosAlumno(int AlumnoID, int MateriaID)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidComentariosAlumno> comentariosRet = new List<AndroidComentariosAlumno>();

            var query = (from scc in dbCtx.StudentCourseComments
                         where scc.CourseID == MateriaID
                         where scc.StudentID == AlumnoID
                         select new
                         {
                             Comment = scc.Description
                         }).ToList();

            foreach (var comment in query)
            {
                AndroidComentariosAlumno comentario = new AndroidComentariosAlumno();
                comentario.Comentario = comment.Comment;

                comentariosRet.Add(comentario);
            }

            return comentariosRet;
        }
    }
}