using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.ViewModels
{
    public class GruposVerGrupoViewModel
    {
        public int ID { get; set; }

        public string GrupoID { get; set; }

        public string Carrera { get; set; }

        public int Cuatrimestre { get; set; }

        public string Seccion { get; set; }

        public string Modalidad { get; set; }
        
        public string Turno { get; set; }

        public int Generacion { get; set; }

        public string Tutor { get; set; }
    }
}