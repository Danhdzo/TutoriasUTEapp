using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidRecordatorios
    {
        public DateTime Fecha { get; set; }

        public string Asunto { get; set; }

        public string Recordatorio { get; set; }
    }
}