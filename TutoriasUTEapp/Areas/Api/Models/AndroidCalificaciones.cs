using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidCalificaciones
    {
        public int AlumnoID { get; set; }

        public int MateriaID { get; set; }

        public int Unidad { get; set; }

        public string Calificacion { get; set; }
    }
}