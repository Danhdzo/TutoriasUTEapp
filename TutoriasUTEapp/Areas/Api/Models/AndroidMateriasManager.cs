using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutoriasUTEapp.Models;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidMateriasManager
    {
        public static List<AndroidMateria> MateriasMaestro(int MaestroID)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidMateria> materias = new List<AndroidMateria>();

            //obtener todas las materias que imparte el maestro
            var queryMaterias = (from c in dbCtx.Courses
                                 where c.TeacherID == MaestroID
                                 select new
                                 {
                                     MateriaID = c.ID,
                                     MateriaDesc = c.Description
                                 }).ToList();

            //seleccionar las materias que estan en uso
            foreach (var materia in queryMaterias)
            {
                var queryMateriaInGroup = (from cgc in dbCtx.ClassGroupCourses
                                           join cg in dbCtx.ClassGroups on cgc.ClassGroupID equals cg.ID
                                           join t in dbCtx.Teachers on cg.TeacherID equals t.ID
                                           where cgc.CourseID == materia.MateriaID
                                           select new
                                           {
                                               GroupID = cg.ID,
                                               GroupLongID = cg.GroupID,
                                               GroupTutor = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName
                                           }).ToList();

                //se crean los objetos Android materia
                foreach (var materiaInGroup in queryMateriaInGroup)
                {
                    AndroidMateria objMateria = new AndroidMateria();

                    //se asignan los valores
                    objMateria.MateriaID = materia.MateriaID;
                    objMateria.MateriaDesc = materia.MateriaDesc;
                    objMateria.GrupoID = materiaInGroup.GroupID;
                    objMateria.GrupoLongID = materiaInGroup.GroupLongID;
                    objMateria.GrupoTutor = materiaInGroup.GroupTutor;

                    //se agrega a la lista
                    materias.Add(objMateria);
                }
            }

            return materias;
        }

        public static List<AndroidMateriaT> MateriasTutor(int MaestroID)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidMateriaT> materias = new List<AndroidMateriaT>();

            //obtener todas las materias que imparte el maestro
            var queryGrupo = (from cg in dbCtx.ClassGroups
                              where cg.TeacherID == MaestroID
                              select new
                              {
                                  GroupID = cg.ID
                              }).SingleOrDefault();

            if (queryGrupo != null)
            {
                var queryMaterias = (from cgc in dbCtx.ClassGroupCourses
                                     join c in dbCtx.Courses on cgc.CourseID equals c.ID
                                     join t in dbCtx.Teachers on c.TeacherID equals t.ID
                                     where cgc.ClassGroupID == queryGrupo.GroupID
                                     select new
                                     {
                                         MateriaID = c.ID,
                                         MateriaDesc = c.Description,
                                         Instructor = t.LastNameP + " " + t.LastNameM + " " + t.FirstMidName
                                     }).ToList();

                //seleccionar las materias que estan en uso
                foreach (var materia in queryMaterias)
                {
                    AndroidMateriaT objMateria = new AndroidMateriaT();

                    //se agregan valores
                    objMateria.MateriaID = materia.MateriaID;
                    objMateria.MateriaDesc = materia.MateriaDesc;
                    objMateria.Instructor = materia.Instructor;

                    //se agrega 
                    materias.Add(objMateria);
                }
            }



            return materias;
        }
    }
}