using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidComentarios
    {
        public int AlumnoID { get; set; }

        public int MateriaID { get; set; }

        public string Comentario { get; set; }
    }
}