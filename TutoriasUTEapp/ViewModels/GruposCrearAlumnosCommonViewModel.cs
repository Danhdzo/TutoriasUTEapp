using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.ViewModels
{
    public class GruposCrearAlumnosCommonViewModel
    {
        public int GrupoID { get; set; }

        public GruposCrearAlumnosCreateViewModel GruposCrearAlumnosCreateVM { get; set; }

        public GruposCrearAlumnosResultViewModel GruposCrearAlumnosResultVM { get; set; }
    }
}