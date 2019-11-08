using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.ViewModels
{
    public class GruposCrearMateriasViewModel
    {
        
        public int ID { get; set; }
        
        public string Description { get; set; }

        public string Maestro { get; set; }

        public int GrupoID { get; set; }
        
        public bool isSelected { get; set; }
    }
}