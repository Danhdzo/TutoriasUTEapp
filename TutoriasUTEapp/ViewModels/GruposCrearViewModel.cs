using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.ViewModels
{
    public class GruposCrearViewModel
    {

        [Required(ErrorMessage = "El campo Carrera es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo Carrera debe ser un numero entero")]
        [Display(Name = "ID de Carrera")]
        public int CareerID { get; set; }
        public List<Models.Career> Carreras { get; set; }

        [Required(ErrorMessage = "El campo Cuatrimestre es obligatorio")]
        [Range(0, 6, ErrorMessage = "El campo Cuatrimestre debe ser un numero entero entre 0 y 6")]
        [Display(Name = "Cuatrimestre")]
        public int Term { get; set; }

        [Required(ErrorMessage = "El campo Sección es obligatorio")]
        [StringLength(2, ErrorMessage = "La longitud es de maximo 2 caracteres")]
        [Display(Name = "Sección")]
        public string Section { get; set; }

        [Required(ErrorMessage = "El campo Modalidad es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Modalidad debe ser un numero entero")]
        [Display(Name = "ID de Modalidad")]
        public int ModalityID { get; set; }
        public List<Models.Modality> Modalidades { get; set; }

        [Required(ErrorMessage = "El campo Turno es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Turno debe ser un numero entero")]
        [Display(Name = "ID de Turno")]
        public int TurnID { get; set; }
        public List<Models.Turn> Turnos { get; set; }

        [Required(ErrorMessage = "El campo Generación es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo Generación debe ser un numero entero")]
        [Display(Name = "Generación")]
        public int Generation { get; set; }

        [Required(ErrorMessage = "El campo Tutor es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Teacher debe ser un numero entero")]
        [Display(Name = "ID de Tutor")]
        public int TutorID { get; set; }
        public List<GruposCrearTutorViewModel> Tutores { get; set; }
    }
}