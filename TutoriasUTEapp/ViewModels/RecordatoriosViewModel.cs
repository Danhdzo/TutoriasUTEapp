using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.ViewModels
{
    public class RecordatoriosViewModel
    {

        public int RecordatorioID { get; set; }

        public DateTime Fecha { get; set; }

        public string Asunto { get; set; }

        public string Recordatorio { get; set; }

        public string Rol { get; set; }

    }
}