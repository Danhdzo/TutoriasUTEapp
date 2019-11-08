using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidMateria
    {
        public int MateriaID { get; set; }

        public string MateriaDesc { get; set; }

        public int GrupoID { get; set; }

        public string GrupoLongID { get; set; }

        public string GrupoTutor { get; set; }
    }
}