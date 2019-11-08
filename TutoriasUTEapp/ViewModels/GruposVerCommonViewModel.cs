using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.ViewModels
{
    public class GruposVerCommonViewModel
    {
        public GruposVerGrupoViewModel Grupo { get; set; }

        public List<GruposVerMateriasViewModel> Materias { get; set; }

        public List<GruposVerAlumnosViewModel> Alumnos { get; set; }
    }
}