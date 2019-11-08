using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TutoriasUTEapp.Models
{
    public class ClassGroup
    {

        [Key]
        public int ID { get; set; }

        //cambiado
        [Required(ErrorMessage = "El campo GroupID es obligatorio")]
        [StringLength(45, ErrorMessage = "La longitud es de maximo 40 caracteres")]
        public string GroupID { get; set; }

        [ForeignKey("Career")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Career debe ser un numero entero")]
        public int CareerID { get; set; }
        public Career Career { get; set; }

        [Required(ErrorMessage = "El campo Term es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Term debe ser un numero entero")]
        public int Term { get; set; }

        [Required(ErrorMessage = "El campo Section es obligatorio")]
        [StringLength(2, ErrorMessage = "La longitud es de maximo 2 caracteres")]
        public string Section { get; set; }

        [ForeignKey("Modality")]
        [Required(ErrorMessage = "El campo Modality es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Modality debe ser un numero entero")]
        public int ModalityID { get; set; }
        public Modality Modality { get; set; }

        [ForeignKey("Turn")]
        [Required(ErrorMessage = "El campo Turn es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Turn debe ser un numero entero")]
        public int TurnID { get; set; }
        public Turn Turn { get; set; }

        [Required(ErrorMessage = "El campo Generation es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Generation debe ser un numero entero")]
        public int Generation { get; set; }

        [ForeignKey("Teacher")]
        [Required(ErrorMessage = "El campo Teacher es obligatorio")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Teacher debe ser un numero entero")]
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public virtual ICollection<ClassGroupCourse> ClassGroupCourses { get; set; }
        public virtual ICollection<ClassGroupStudent> ClassGroupStudents { get; set; }

    }
}