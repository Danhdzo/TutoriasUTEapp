using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.ViewModels
{
    public class RecordatoriosResultViewModel
    {
        [Display(Name = "Materias")]
        public List<RecordatoriosViewModel> Recordatorios { get; set; }

    }
}