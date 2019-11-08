using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TutoriasUTEapp.ViewModels
{
    public class MaestrosCreateViewModel
    {
        [Required(ErrorMessage = "El campo ID de Empleado es obligatorio")]
        [Display(Name = "ID de Empleado")]
        public string EmployeeID { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [Display(Name = "Nombre")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        [Display(Name = "Apellido Paterno")]
        public string LastNameP { get; set; }

        [Required(ErrorMessage = "El campo Apellido Materno es obligatorio")]
        [Display(Name = "Apellido Materno")]
        public string LastNameM { get; set; }

        [Required(ErrorMessage = "El campo Usuario es obligatorio")]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
        
        [Display(Name = "Tutor")]
        public bool Tutor { get; set; }
    }
}