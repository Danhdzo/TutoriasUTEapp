using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TutoriasUTEapp.ViewModels
{
    public class MateriasCreateViewModel
    {
        [Required(ErrorMessage = "El Nombre de la materia es obligatorio")]
        [Display(Name = "Nombre")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El número de Unidades es obligatorio")]
        [Display(Name = "Unidades")]
        [Range(1, 10, ErrorMessage = "El campo Unidades debe ser un numero entero del 1 al 10")]
        public int Units { get; set; }

        [Required(ErrorMessage = "El ID de empleado es obligatorio")]
        [Display(Name = "Docente")]
        public string EmployeeID { get; set; }
        //tener un list de de empleados para mostrarlos en la vista?



    }
}