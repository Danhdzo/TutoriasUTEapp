using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TutoriasUTEapp.Models;

namespace TutoriasUTEapp.ViewModels
{
    public class GruposCrearAlumnosCreateViewModel
    {
        [Required(ErrorMessage = "El campo Matrícula es obligatorio")]
        [StringLength(15, ErrorMessage = "La longitud es de 15 caracteres")]
        [Display(Name = "Matrícula")]
        public string Registration { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [StringLength(25, ErrorMessage = "La longitud es de 25 caracteres")]
        [Display(Name = "Nombre")]
        public string FirstMidName { get; set; }

        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        [StringLength(25, ErrorMessage = "La longitud es de 25 caracteres")]
        [Display(Name = "Apellido Paterno")]
        public string LastNameP { get; set; }

        [Required(ErrorMessage = "El campo Apellido Materno es obligatorio")]
        [StringLength(25, ErrorMessage = "La longitud es de 25 caracteres")]
        [Display(Name = "Apellido Materno")]
        public string LastNameM { get; set; }

        [StringLength(20, ErrorMessage = "La longitud es de 20 caracteres")]
        [Display(Name = "Teléfono")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "El campo Telefono de Emergencia es obligatorio")]
        [StringLength(20, ErrorMessage = "La longitud es de 20 caracteres")]
        [Display(Name = "Telefono de Emergencia")]
        public string EmergencyTelephone { get; set; }

        [Required(ErrorMessage = "El campo Email Académico es obligatorio")]
        [StringLength(40, ErrorMessage = "La longitud es de 40 caracteres")]
        [Display(Name = "Email Academico")]
        public string AcademicEmail { get; set; }

        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio")]
        [StringLength(20, ErrorMessage = "La longitud es de 20 caracteres")]
        [Display(Name = "Fecha de Nacimiento")]
        public string Birthday { get; set; }

        [Display(Name = "ID de Situación")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo ID de Situación debe ser un numero entero")]
        public int SituationID { get; set; }
        public List<Situation> Situaciones { get; set; }
    }
}