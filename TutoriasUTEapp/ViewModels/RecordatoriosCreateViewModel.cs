using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.ViewModels
{
    public class RecordatoriosCreateViewModel
    {
        [Display(Name = "Fecha")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:G}")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El asunto del recordatorio es obligatorio")]
        [Display(Name = "Asunto")]
        public string Asunto { get; set; }

        [Required(ErrorMessage = "La descripción del recordatorio es obligatorio")]
        [Display(Name = "Descripción")]
        public string Recordatorio { get; set; }

        [Display(Name = "Rol al que aplica")]
        public string Rol { get; set; }
    }
}